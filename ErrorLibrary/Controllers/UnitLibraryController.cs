using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UnitLibraryController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public UnitLibraryController(IUnitService unitService, ISharedService sharedService, IMapper mapper)
        {
            _unitService = unitService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUnits()
        {
            var units = await _unitService.GetAll();
            return Json(_mapper.Map<List<UnitDto>>(units));

        }

        public async Task<IActionResult> GetUnitById(int id)
        {
            var unit = await _unitService.GetById(id);
            return Json(_mapper.Map<List<UnitDto>>(unit));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UnitDto unitDto)
        {
            if(await _unitService.CheckNameExists(unitDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _unitService.Add(_mapper.Map<Unit>(unitDto));
            if(await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UnitDto unitDto)
        {
            var unit = await _unitService.GetById(unitDto.Id);
            if(unit == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _unitService.CheckNameExists(unitDto.Name) && unitDto.Name != unit.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _unitService.Update(_mapper.Map(unitDto, unit));
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
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var unit = await _unitService.GetById(id);
            if (unit == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _unitService.Delete(unit);
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
