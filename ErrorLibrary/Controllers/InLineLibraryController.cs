using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class InLineLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IInLineService _inLineService;
        private readonly ILineService _lineService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public InLineLibraryController(ISharedService sharedService, IInLineService inLineService, IMapper mapper, ILineService lineService, IProductService productService, IUserService userService)
        {
            _sharedService = sharedService;
            _inLineService = inLineService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _lineService = lineService;
            _productService = productService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetInLines()
        {
            var inLines = await _inLineService.GetAll();
            var lines = await _lineService.GetAll();
            var products = await _productService.GetAll();
            var users = await _userService.GetAll();
            var inLinesDto = _mapper.Map<List<InLineDisplayDto>>(inLines);
            foreach (var inLine in inLinesDto)
            {
                var line = lines.FirstOrDefault(l => l.Id == inLine.LineId) ?? new Line();
                var product = products.FirstOrDefault(p => p.Id == inLine.ProductId) ?? new Product();
                var user = users.FirstOrDefault(u => u.Id == inLine.UserId) ?? new ApplicationUser();
                inLine.Line = _mapper.Map<LineDto>(line);
                inLine.Product = _mapper.Map<ProductDto>(product);
                inLine.User = _mapper.Map<UserDto>(user);
            }
            _responseDto.Result = inLinesDto;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetInLineById(int id)
        {
            var inLine = await _inLineService.GetById(id);
            _responseDto.Result = _mapper.Map<InLineDto>(inLine);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CheckInitAndUpdate([FromBody] InitAndUpdateInLineDto initAndUpdateInLineDto)
        {
            var inLines = await _inLineService.GetAll();
            var existingKeys = _inLineService.BuildExistingInLineKeySet(inLines);
            var keyToCheck = _inLineService.CheckNameExistsFast(existingKeys, initAndUpdateInLineDto.LineId, initAndUpdateInLineDto.ProductId, initAndUpdateInLineDto.UserId, initAndUpdateInLineDto.Date);
            InLine inLine;
            if (keyToCheck)
            {
                inLine = (inLines.FirstOrDefault(inLines =>
                    inLines.LineId == initAndUpdateInLineDto.LineId &&
                    inLines.ProductId == initAndUpdateInLineDto.ProductId &&
                    inLines.UserId == initAndUpdateInLineDto.UserId &&
                    inLines.Date == initAndUpdateInLineDto.Date)) ?? new InLine();
                // Nếu là load lần đầu thì chỉ trả về thông tin 'In line'
                if (initAndUpdateInLineDto.FirstLoad)
                {
                    _responseDto.Message = "Lấy số lượng 'số lượng kiểm' cho lần load đầu tiên";
                    _responseDto.Result = _mapper.Map<InLineDto>(inLine);
                    return Json(_responseDto);
                }
                // Không phải load lần đầu thì cập nhật
                initAndUpdateInLineDto.Id = inLine.Id;
                _inLineService.Update(_mapper.Map(initAndUpdateInLineDto, inLine));
                _responseDto.Message = "Cập nhật 'In line' thành công";
            }
            else
            {
                inLine = _mapper.Map<InLine>(initAndUpdateInLineDto);
                _inLineService.Add(inLine);
                _responseDto.Message = "Khởi tạo 'In line' thành công";
            }
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Result = _mapper.Map<InLineDto>(inLine);
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình khởi tạo hoặc cập nhật 'In line'";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddInLine([FromBody] InLineDto inLineDto)
        {
            if (await _inLineService.CheckExists(inLineDto.LineId, inLineDto.ProductId, inLineDto.UserId, inLineDto.Date))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _inLineService.Add(_mapper.Map<InLine>(inLineDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInLine([FromBody] InLineDto inLineDto)
        {
            var inLine = await _inLineService.GetById(inLineDto.Id);
            if (inLine == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _inLineService.CheckExists(inLineDto.LineId, inLineDto.ProductId, inLineDto.UserId, inLineDto.Date) &&
                (inLineDto.LineId != inLine.LineId || inLineDto.ProductId != inLine.ProductId || inLineDto.UserId != inLine.UserId || inLineDto.Date != inLine.Date);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _inLineService.Update(_mapper.Map(inLineDto, inLine));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInLine([FromBody] int id)
        {
            var inLine = await _inLineService.GetById(id);
            if (inLine == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _inLineService.Delete(inLine);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
