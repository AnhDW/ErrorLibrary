using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper.Enums;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ReportFinalFactoryDetailLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IReportFinalFactoryService _reportFinalFactoryService;
        private readonly IReportFinalFactoryDetailService _reportFinalFactoryDetailService;
        private readonly IReportFinalFactoryDetailDefectService _reportFinalFactoryDetailDefectService;
        private readonly ICustomerService _customerService;
        private readonly IStyleService _styleService;
        private readonly IDefectService _defectService;
        private readonly IFactoryService _factoryService;

        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryDetailLibraryController(ISharedService sharedService, IReportFinalFactoryService reportFinalFactoryService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IReportFinalFactoryDetailDefectService reportFinalFactoryDetailDefectService, ICustomerService customerService, IStyleService styleService, IDefectService defectService, IFactoryService factoryService, IMapper mapper)
        {
            _sharedService = sharedService;
            _reportFinalFactoryService = reportFinalFactoryService;
            _reportFinalFactoryDetailService = reportFinalFactoryDetailService;
            _reportFinalFactoryDetailDefectService = reportFinalFactoryDetailDefectService;
            _customerService = customerService;
            _styleService = styleService;
            _defectService = defectService;
            _factoryService = factoryService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetByReportFinalFactory(int reportFinalFactoryId)
        {
            var reportFinalFactoryDetails = await _reportFinalFactoryDetailService.GetByReportFinalFactoryId(reportFinalFactoryId);
            var customers = await _customerService.GetAll();
            var styles = await _styleService.GetAll();

            var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetAll();
            var result = _mapper.Map<List<ReportFinalFactoryDetailGridDto>>(reportFinalFactoryDetails);
            foreach (var reportFinalFactoryDetail in result)
            {
                reportFinalFactoryDetail.CustomerCode = customers.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.CustomerId)!.Code;
                reportFinalFactoryDetail.StyleCode = styles.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.StyleId)!.Code;
                reportFinalFactoryDetail.ReportFinalFactoryDetailDefects = _mapper.Map<List<ReportFinalFactoryDetailDefectDto>>(reportFinalFactoryDetailDefects.Where(x => x.ReportFinalFactoryDetailId == reportFinalFactoryDetail.Id));
            }
            _responseDto.Result = result;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportFinalFactoryDetail([FromBody] ReportFinalFactoryDetailDto reportFinalFactoryDetailDto)
        {
            var reportFinalFactory = await _reportFinalFactoryService.GetById(reportFinalFactoryDetailDto.ReportFinalFactoryId);
            if (reportFinalFactory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactory not found.";
                return Json(_responseDto);
            }
            var customer = await _customerService.GetByCode(reportFinalFactoryDetailDto.CustomerCode);
            var style = await _styleService.GetByCode(reportFinalFactoryDetailDto.StyleCode);
            var reportFinalFactoryDetailDefects = await InitialReportFinalFactoryDetailDefects();

            var reportFinalFactoryDetail = new ReportFinalFactoryDetail
            {
                ReportFinalFactoryId = reportFinalFactoryDetailDto.ReportFinalFactoryId,
                PO = reportFinalFactoryDetailDto.PO,
                Quantity = reportFinalFactoryDetailDto.Quantity,
                ReportFinalFactoryDetailDefects = reportFinalFactoryDetailDefects,
                Customer = customer,
                Style = style
            };

            _reportFinalFactoryDetailService.Add(reportFinalFactoryDetail);

            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail created successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to create ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReportFinalFactoryDetail([FromBody] ReportFinalFactoryDetailDto reportFinalFactoryDetailDto)
        {
            var reportFinalFactoryDetail = await _reportFinalFactoryDetailService.GetById(reportFinalFactoryDetailDto.Id);
            if (reportFinalFactoryDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactoryDetail not found.";
                return Json(_responseDto);
            }
            var customer = await _customerService.GetByCode(reportFinalFactoryDetailDto.CustomerCode);
            var style = await _styleService.GetByCode(reportFinalFactoryDetailDto.StyleCode);
            _mapper.Map(reportFinalFactoryDetailDto, reportFinalFactoryDetail);
            reportFinalFactoryDetail.Customer = customer;
            reportFinalFactoryDetail.Style = style;
            _reportFinalFactoryDetailService.Update(reportFinalFactoryDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail update successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to update ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReportFinalFactoryDetail([FromBody] int id)
        {
            var reportFinalFactoryDetail = await _reportFinalFactoryDetailService.GetById(id);
            if(reportFinalFactoryDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactoryDetail not found.";
                return Json(_responseDto);
            }
            _reportFinalFactoryDetailService.Delete(reportFinalFactoryDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail deleted successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to delete ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        private async Task<List<ReportFinalFactoryDetailDefect>> InitialReportFinalFactoryDetailDefects()
        {
            var defects = await _defectService.GetAll();
            var reportFinalFactoryDetailDefects = new List<ReportFinalFactoryDetailDefect>();
            foreach (var defect in defects)
            {
                reportFinalFactoryDetailDefects.Add(new ReportFinalFactoryDetailDefect
                {
                    DefectId = defect.Id,
                    Quantity = 0
                });
            }
            return reportFinalFactoryDetailDefects;
        }

    }
}
