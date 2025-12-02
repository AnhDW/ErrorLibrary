using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Extensions;
using ErrorLibrary.Helper.EntityParams;
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

        public async Task<IActionResult> GetErrorGroupsPagination([FromQuery] ErrorGroupParams errorGroupParams)
        {
            var result = await _errorGroupService.GetAll(errorGroupParams);
            Response.AddPaginationHeader(new Helper.PaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages));
            _responseDto.Result = result;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetErrorGroups()
        {
            var errorGroups = await _errorGroupService.GetAll();
            _responseDto.Result = _mapper.Map<List<ErrorGroupDto>>(errorGroups.OrderBy(x => x.Code));
            return Json(_responseDto);

        }

        public async Task<IActionResult> GetErrorGroupById(int id)
        {
            var errorGroup = await _errorGroupService.GetById(id);
            return Json(_mapper.Map<ErrorGroupDto>(errorGroup));
        }

        public async Task<IActionResult> GenerateErrorGroupCode()
        {
            var existingCodes = await _errorGroupService.GetAllCodes();
            var nextCode = _errorGroupService.GetNextErrorGroupCode(existingCodes);
            _responseDto.Result = nextCode;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GenerateErrorGroupCodeWhenUpdate(string currentCode)
        {
            var existingCodes = await _errorGroupService.GetAllCodes();
            existingCodes = existingCodes.Where(x => x != currentCode).ToList();
            var nextCode = _errorGroupService.GetNextErrorGroupCode(existingCodes);
            _responseDto.Result = nextCode;
            return Json(_responseDto);
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

            if (await _errorGroupService.CheckCodeExists(errorGroupDto.Code))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Mã nhóm lỗi đã tồn tại";
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
            bool isCodeExists = await _errorGroupService.CheckCodeExists(errorGroupDto.Code) && errorGroupDto.Code != errorGroup.Code;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên nhóm lỗi đã tồn tại";
                return Json(_responseDto);
            }

            if (isCodeExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Mã nhóm lỗi đã tồn tại";
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

        [HttpPost]
        public async Task<IActionResult> AddErrorGroupByNames([FromBody] List<string> names)
        {
            if (!names.Any())
            {
                _responseDto.Message = "Không có nhóm lỗi mới nào được thêm";
                return Json(_responseDto);
            }
            var existingErrorGroups = await _errorGroupService.GetByNames(names);
            var existingNames = existingErrorGroups.Select(x => x.Name).ToList();
            var newNames = names.Except(existingNames).ToList();
            var existingCodes = await _errorGroupService.GetAllCodes();
            foreach (var name in newNames)
            {
                var nextCode = _errorGroupService.GetNextErrorGroupCode(existingCodes);
                var newErrorGroup = new ErrorGroup
                {
                    Name = name,
                    Code = nextCode,
                    Description = string.Empty
                };
                existingCodes.Add(nextCode);
                _errorGroupService.Add(newErrorGroup);
            }
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = $"Đã thêm {newNames.Count} nhóm lỗi thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }
    }
}
