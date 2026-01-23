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
        private readonly IInspectionService _inspectionService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryDetailLibraryController(ISharedService sharedService, IReportFinalFactoryService reportFinalFactoryService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IFactoryService factoryService, IMapper mapper, IDefectService defectService, ICustomerService customerService, IStyleService styleService, IInspectionService inspectionService, IReportFinalFactoryDetailDefectService reportFinalFactoryDetailDefectService)
        {
            _sharedService = sharedService;
            _reportFinalFactoryService = reportFinalFactoryService;
            _reportFinalFactoryDetailService = reportFinalFactoryDetailService;
            _factoryService = factoryService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _defectService = defectService;
            _customerService = customerService;
            _styleService = styleService;
            _inspectionService = inspectionService;
            _reportFinalFactoryDetailDefectService = reportFinalFactoryDetailDefectService;
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
            var inspections = await _inspectionService.GetAll();
            var result = _mapper.Map<List<ReportFinalFactoryDetailDisplayDto>>(reportFinalFactoryDetails);
            foreach (var reportFinalFactoryDetail in result)
            {
                reportFinalFactoryDetail.Customer = _mapper.Map<CustomerDto>(customers.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.CustomerId)!);
                reportFinalFactoryDetail.Style = _mapper.Map<StyleDto>(styles.FirstOrDefault(s => s.Id == reportFinalFactoryDetail.StyleId)!);
                reportFinalFactoryDetail.Inspections = _mapper.Map<List<InspectionDisplayDto>>(inspections.Where(i => i.ReportFinalFactoryDetailId == reportFinalFactoryDetail.Id).ToList());
            }
            _responseDto.Result = result;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReportFinalFactoryDetailDto reportFinalFactoryDetailDto)
        {
            var reportFinalFactory = await _reportFinalFactoryService.GetById(reportFinalFactoryDetailDto.ReportFinalFactoryId);
            if (reportFinalFactory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactory not found.";
                return Json(_responseDto);
            }

            var inspections = InitialInspections();
            var reportFinalFactoryDetailDefects = await InitialReportFinalFactoryDetailDefects();

            var reportFinalFactoryDetail = new ReportFinalFactoryDetail
            {
                ReportFinalFactoryId = reportFinalFactoryDetailDto.ReportFinalFactoryId,
                CustomerId = reportFinalFactoryDetailDto.CustomerId,
                StyleId = reportFinalFactoryDetailDto.StyleId,
                PO = reportFinalFactoryDetailDto.PO,
                Quantity = reportFinalFactoryDetailDto.Quantity,
                Inspections = inspections,
                ReportFinalFactoryDetailDefects = reportFinalFactoryDetailDefects
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

        private List<Inspection> InitialInspections()
        {
            var inspections = new List<Inspection>() {
                new Inspection { InspectionType = InspectionType.PreFinal,
                    InspectionRounds = new List<InspectionRound>(){
                        new InspectionRound() { Name = "Lần 1" },
                        new InspectionRound() { Name = "Lần 2" },
                        new InspectionRound() { Name = "Lần 3" }
                    }
                },
                new Inspection { InspectionType = InspectionType.Final,
                    InspectionRounds = new List<InspectionRound>(){
                        new InspectionRound() { Name = "Lần 1" },
                        new InspectionRound() { Name = "Lần 2" },
                        new InspectionRound() { Name = "Lần 3" }
                    }
                }
            };
            return inspections;
        }

        private async Task<List<ReportFinalFactoryDetailDefect>> InitialReportFinalFactoryDetailDefects()
        {
            var defects = await _defectService.GetAll();
            var reportFinalFactoryDetailDefects = new List<ReportFinalFactoryDetailDefect>();
            foreach (var defect in defects) {
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
