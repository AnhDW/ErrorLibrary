using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class DefectLibraryController : Controller
    {
        private readonly IDefectService _defectService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public DefectLibraryController(IDefectService defectService, ISharedService sharedService, IMapper mapper)
        {
            _defectService = defectService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDefects()
        {
            var defects = await _defectService.GetAll();
            _responseDto.Result = _mapper.Map<List<DefectDto>>(defects);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetDefectById(int id)
        {
            var defect = await _defectService.GetById(id);
            _responseDto.Result = _mapper.Map<DefectDto>(defect);
            return Json(_responseDto);
        }

        //[HasPermission("Defects", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddDefect([FromBody] DefectDto defectDto)
        {
            if (await _defectService.CheckNameExists(defectDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên khuyết điểm đã tồn tại";
                return Json(_responseDto);
            }

            _defectService.Add(_mapper.Map<Defect>(defectDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm khuyết điểm thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        //[HasPermission("Defects", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateDefect([FromBody] DefectDto defectDto)
        {
            var defect = await _defectService.GetById(defectDto.Id);
            if (defect == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'khuyết điểm' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _defectService.CheckNameExists(defectDto.Name) && defectDto.Name != defect.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên khuyết điểm đã tồn tại";
                return Json(_responseDto);
            }

            _defectService.Update(_mapper.Map(defectDto, defect));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật khuyết điểm thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        //[HasPermission("Defects", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteDefect([FromBody] int id)
        {
            var defect = await _defectService.GetById(id);
            if (defect == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'khuyết điểm' này trong thư viện";
                return Json(_responseDto);
            }

            _defectService.Delete(defect);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa khuyết điểm thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
