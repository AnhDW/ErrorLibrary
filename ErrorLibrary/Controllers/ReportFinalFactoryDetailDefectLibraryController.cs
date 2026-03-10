using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ReportFinalFactoryDetailDefectLibraryController : Controller
    {
        private readonly IReportFinalFactoryDetailDefectService _reportFinalFactoryDetailDefectService;
        private readonly IReportFinalFactoryDetailService _reportFinalFactoryDetailService;
        private readonly IDefectService _defectService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryDetailDefectLibraryController(IReportFinalFactoryDetailDefectService reportFinalFactoryDetailDefectService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IDefectService defectService, ISharedService sharedService, IMapper mapper)
        {
            _reportFinalFactoryDetailDefectService = reportFinalFactoryDetailDefectService;
            _reportFinalFactoryDetailService = reportFinalFactoryDetailService;
            _defectService = defectService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetReportFinalFactoryDetailDefects()
        {
            var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetAll();
            _responseDto.Result = _mapper.Map<List<ReportFinalFactoryDetailDefectDto>>(reportFinalFactoryDetailDefects);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetReportFinalFactoryDetailDefectById(int reportFinalFactoryDetailId, int defectId)
        {
            var reportFinalFactoryDetailDefect = await _reportFinalFactoryDetailDefectService.GetById(reportFinalFactoryDetailId, defectId);
            _responseDto.Result = _mapper.Map<ReportFinalFactoryDetailDefectDto>(reportFinalFactoryDetailDefect);
            return Json(_responseDto);
        }

        //[HasPermission("ReportFinalFactoryDetailDefects", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddReportFinalFactoryDetailDefect([FromBody] ReportFinalFactoryDetailDefectDto reportFinalFactoryDetailDefectDto)
        {
            if (await _reportFinalFactoryDetailDefectService.CheckExists(reportFinalFactoryDetailDefectDto.ReportFinalFactoryDetailId, reportFinalFactoryDetailDefectDto.DefectId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _reportFinalFactoryDetailDefectService.Add(_mapper.Map<ReportFinalFactoryDetailDefect>(reportFinalFactoryDetailDefectDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        //[HasPermission("ReportFinalFactoryDetailDefects", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateReportFinalFactoryDetailDefect([FromBody] ReportFinalFactoryDetailDefectDto reportFinalFactoryDetailDefectDto)
        {
            var reportFinalFactoryDetailDefect = await _reportFinalFactoryDetailDefectService.GetById(reportFinalFactoryDetailDefectDto.ReportFinalFactoryDetailId, reportFinalFactoryDetailDefectDto.DefectId);
            if (reportFinalFactoryDetailDefect == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _reportFinalFactoryDetailDefectService.Update(_mapper.Map(reportFinalFactoryDetailDefectDto, reportFinalFactoryDetailDefect));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        //[HasPermission("ReportFinalFactoryDetailDefects", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteReportFinalFactoryDetailDefect([FromBody] ReportFinalFactoryDetailDefectDto reportFinalFactoryDetailDefectDto)
        {
            var reportFinalFactoryDetailDefect = await _reportFinalFactoryDetailDefectService.GetById(reportFinalFactoryDetailDefectDto.ReportFinalFactoryDetailId, reportFinalFactoryDetailDefectDto.DefectId);
            if (reportFinalFactoryDetailDefect == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _reportFinalFactoryDetailDefectService.Delete(reportFinalFactoryDetailDefect);
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
