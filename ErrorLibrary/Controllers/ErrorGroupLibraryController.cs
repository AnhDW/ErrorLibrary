using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorGroupLibraryController : Controller
    {
        private readonly IErrorGroupService _errorGroupService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorGroupLibraryController(IErrorGroupService errorGroupService, ISharedService sharedService, IMapper mapper)
        {
            _errorGroupService = errorGroupService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetErrorGroups()
        {
            var errorGroups = await _errorGroupService.GetAll();
            return Json(_mapper.Map<List<ErrorGroupDto>>(errorGroups));

        }

        public async Task<IActionResult> GetErrorGroupById(int id)
        {
            var errorGroup = await _errorGroupService.GetById(id);
            return Json(_mapper.Map<ErrorGroupDto>(errorGroup));
        }

        [HttpPost]
        public async Task<IActionResult> AddErrorGroup([FromBody] ErrorGroupDto errorGroupDto)
        {
            if (await _errorGroupService.CheckNameExists(errorGroupDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên nhóm lỗi đã tồn tại";
                return Json(_responseDto);
            }

            _errorGroupService.Add(_mapper.Map<ErrorGroup>(errorGroupDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateErrorGroup([FromBody] ErrorGroupDto errorGroupDto)
        {
            var errorGroup = await _errorGroupService.GetById(errorGroupDto.Id);
            if (errorGroup == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'nhóm lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _errorGroupService.CheckNameExists(errorGroupDto.Name) && errorGroupDto.Name != errorGroup.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên nhóm lỗi đã tồn tại";
                return Json(_responseDto);
            }

            _errorGroupService.Update(_mapper.Map(errorGroupDto, errorGroup));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật nhóm lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteErrorGroup([FromBody] int id)
        {
            var errorGroup = await _errorGroupService.GetById(id);
            if (errorGroup == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'nhóm lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            _errorGroupService.Delete(errorGroup);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa nhóm lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
