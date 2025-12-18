using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ErrorLibrary.Controllers
{
    public class ReportLibraryController : Controller
    {
        private readonly IInLineService _inLineService;
        private readonly IInLineDetailService _inLineDetailService;
        private readonly IEndLineService _endLineService;
        private readonly IEndLineDetailService _endLineDetailService;
        private readonly IProductService _productService;
        private readonly IErrorService _errorService;
        private readonly ILineService _lineService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportLibraryController(IInLineService inLineService, IInLineDetailService inLineDetailService, IErrorService errorService, ILineService lineService, IMapper mapper, IUserService userService, IProductService productService, IEndLineService endLineService, IEndLineDetailService endLineDetailService)
        {
            _inLineService = inLineService;
            _inLineDetailService = inLineDetailService;
            _errorService = errorService;
            _lineService = lineService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _userService = userService;
            _productService = productService;
            _endLineService = endLineService;
            _endLineDetailService = endLineDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReportInLine([FromBody] ReportInLineParams reportInLineParams)
        {
            var inLines = await _inLineService.GetAll();
            var inLineDetails = await _inLineDetailService.GetAll();
            var lines = await _lineService.GetAll();
            var products = await _productService.GetAll();
            var users = await _userService.GetAll();
            if(reportInLineParams.StartDate.HasValue)
            {
                inLines = inLines.Where(i => i.Date >= reportInLineParams.StartDate.Value).ToList();
            }
            if(reportInLineParams.EndDate.HasValue)
            {
                inLines = inLines.Where(i => i.Date <= reportInLineParams.EndDate.Value).ToList();
            }
            var inLinesDto = _mapper.Map<List<InLineDisplayDto>>(inLines);
            
            foreach (var inLine in inLinesDto)
            {
                var line = lines.FirstOrDefault(l => l.Id == inLine.LineId) ?? new Line();
                var product = products.FirstOrDefault(p => p.Id == inLine.ProductId) ?? new Product();
                var user = users.FirstOrDefault(u => u.Id == inLine.UserId) ?? new ApplicationUser();
                var totalErrors = inLineDetails.Where(d => d.InLineId == inLine.Id).Sum(d => d.Quantity);
                inLine.Line = _mapper.Map<LineDto>(line);
                inLine.Product = _mapper.Map<ProductDto>(product);
                inLine.User = _mapper.Map<UserDto>(user);
                inLine.TotalErrors = totalErrors;
            }

            _responseDto.Result = inLinesDto;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> ReportEndLine([FromBody] ReportEndLineParams reportEndLineParams)
        {
            var endLines = await _endLineService.GetAll();
            var products = await _productService.GetAll();
            var endLineDetails = await _endLineDetailService.GetAll();
            var lines = await _lineService.GetAll();
            if(reportEndLineParams.StartDate.HasValue)
            {
                endLines = endLines.Where(i => i.Date >= reportEndLineParams.StartDate.Value).ToList();
            }
            if(reportEndLineParams.EndDate.HasValue)
            {
                endLines = endLines.Where(i => i.Date <= reportEndLineParams.EndDate.Value).ToList();
            }
            var endLinesDto = _mapper.Map<List<EndLineDisplayDto>>(endLines);
            foreach (var endLine in endLinesDto)
            {
                var totalErrors = endLineDetails.Where(x => x.EndLineId == endLine.Id).Count();
                var product = products.FirstOrDefault(x => x.Id == endLine.ProductId);
                var line = lines.FirstOrDefault(x => x.Id == endLine.LineId);
                endLine.TotalErrors = totalErrors;
                endLine.Product = _mapper.Map<ProductDto>(product);
                endLine.Line = _mapper.Map<LineDto>(line);
            }

            _responseDto.Result = endLinesDto;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> InLineErrorChart([FromBody] ReportInLineParams reportInLineParams)
        {
            var errors = await _errorService.GetAll();
            var inLines = await _inLineService.GetAll();
            var inLineDetails = await _inLineDetailService.GetAll();
            if (reportInLineParams.StartDate.HasValue)
            {
                inLines = inLines.Where(i => i.Date >= reportInLineParams.StartDate.Value).ToList();
            }
            if (reportInLineParams.EndDate.HasValue)
            {
                inLines = inLines.Where(i => i.Date <= reportInLineParams.EndDate.Value).ToList();
            }
            inLineDetails = inLineDetails.Where(d => inLines.Any(i => i.Id == d.InLineId)).ToList();
            var quantityErrors = inLineDetails
                .GroupBy(d => d.ErrorId)
                .Select(g => new
                {
                    ErrorId = g.Key,
                    ErrorQuantity = g.Sum(d => d.Quantity)
                })
                .OrderByDescending(q => q.ErrorQuantity).Take(reportInLineParams.RowTake ?? 3)
                .ToList();
            var errorIds = quantityErrors.Select(q => q.ErrorId).ToList();
            var errorNames = errors.Where(e => errorIds.Contains(e.Id)).Select(e => e.Name).ToList();
            var errorCodes = errors.Where(e => errorIds.Contains(e.Id)).Select(e => e.Code).ToList();
            var errorQuantities = quantityErrors.Select(q => q.ErrorQuantity);

            _responseDto.Result = new
            {
                ErrorNames = errorNames,
                ErrorCodes = errorCodes,
                ErrorQuantities = errorQuantities,
            };

            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> EndLineErrorChart([FromBody] ReportEndLineParams reportEndLineParams)
        {
            var errors = await _errorService.GetAll();
            var endLines = await _endLineService.GetAll();
            var endLineDetails = await _endLineDetailService.GetAll();
            if (reportEndLineParams.StartDate.HasValue)
            {
                endLines = endLines.Where(i => i.Date >= reportEndLineParams.StartDate.Value).ToList();
            }
            if (reportEndLineParams.EndDate.HasValue)
            {
                endLines = endLines.Where(i => i.Date <= reportEndLineParams.EndDate.Value).ToList();
            }
            endLineDetails = endLineDetails.Where(d => endLines.Any(i => i.Id == d.EndLineId)).ToList();
            var quantityErrors = endLineDetails
                .GroupBy(d => d.ErrorId)
                .Select(g => new
                {
                    ErrorId = g.Key,
                    ErrorQuantity = g.Count()
                })
                .OrderByDescending(q => q.ErrorQuantity).Take(reportEndLineParams.RowTake ?? 3)
                .ToList();
            var errorIds = quantityErrors.Select(q => q.ErrorId).ToList();
            var errorNames = errors.Where(e => errorIds.Contains(e.Id)).Select(e => e.Name).ToList();
            var errorCodes = errors.Where(e => errorIds.Contains(e.Id)).Select(e => e.Code).ToList();
            var errorQuantities = quantityErrors.Select(q => q.ErrorQuantity);
            
            _responseDto.Result = new
            {
                ErrorNames = errorNames,
                ErrorCodes = errorCodes,
                ErrorQuantities = errorQuantities,
            };

            return Json(_responseDto);
        }
    }
}
