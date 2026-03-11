using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ReportFinalFactoryLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IReportFinalFactoryService _reportFinalFactoryService;
        private readonly IReportFinalFactoryDetailService _reportFinalFactoryDetailService;
        private readonly IFactoryService _factoryService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryLibraryController(ISharedService sharedService, IReportFinalFactoryService reportFinalFactoryService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IMapper mapper, IFactoryService factoryService)
        {
            _sharedService = sharedService;
            _reportFinalFactoryService = reportFinalFactoryService;
            _reportFinalFactoryDetailService = reportFinalFactoryDetailService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _factoryService = factoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportFinalFactory([FromBody] CreateReportFinalFactoryDto createReportFinalFactoryDto)
        {
            var factory = await _factoryService.GetById(createReportFinalFactoryDto.FactoryId);
            if (factory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Factory not found.";
                return Json(_responseDto);
            }
            if (await _reportFinalFactoryService.CheckExists(createReportFinalFactoryDto.FactoryId, createReportFinalFactoryDto.CreatedDate))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "A ReportFinalFactory with the same FactoryId and CreatedDate already exists.";
                return Json(_responseDto);
            }

            _reportFinalFactoryService.Add(new Entities.ReportFinalFactory
            {
                FactoryId = createReportFinalFactoryDto.FactoryId,
                Name = $"Report for {factory.Name} on {createReportFinalFactoryDto.CreatedDate}",
                CreateDate = createReportFinalFactoryDto.CreatedDate
            });

            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactory created successfully.";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Error occurred while creating ReportFinalFactory.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CheckInitReportFinalFactory([FromBody] CreateReportFinalFactoryDto createReportFinalFactoryDto)
        {
            var factory = await _factoryService.GetById(createReportFinalFactoryDto.FactoryId);
            if (factory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Factory not found.";
                return Json(_responseDto);
            }
            var reportFinalFactory = await _reportFinalFactoryService.GetByFactoryIdAndCreateDate(createReportFinalFactoryDto.FactoryId, createReportFinalFactoryDto.CreatedDate);
            if (reportFinalFactory != null)
            {
                _responseDto.Result = _mapper.Map<ReportFinalFactoryDto>(reportFinalFactory);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "A ReportFinalFactory with the same FactoryId and CreatedDate already exists.";
                return Json(_responseDto);
            }

            var newReportFinalFactory = new Entities.ReportFinalFactory
            {
                FactoryId = createReportFinalFactoryDto.FactoryId,
                Name = $"Report for {factory.Name} on {createReportFinalFactoryDto.CreatedDate}",
                CreateDate = createReportFinalFactoryDto.CreatedDate
            };
            _reportFinalFactoryService.Add(newReportFinalFactory);

            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Result = _mapper.Map<ReportFinalFactoryDto>(newReportFinalFactory);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactory created successfully.";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Error occurred while creating ReportFinalFactory.";
            return Json(_responseDto);
        }
    }
}
