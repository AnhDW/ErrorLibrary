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
    public class ErrorCategoryLibraryController : Controller
    {
        private readonly IErrorCategoryService _errorCategoryService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorCategoryLibraryController(IErrorCategoryService errorCategoryService, ISharedService sharedService, IMapper mapper)
        {
            _errorCategoryService = errorCategoryService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            return View();
        }
        //[HasPermission("Enterprises", "Create")]
        //[HasPermission("Enterprises", "Update")]
        //[HasPermission("Enterprises", "Delete")]
        //[HasPermission("Enterprises", "View")]
        public async Task<IActionResult> GetErrorCategorysPagination([FromQuery] ErrorCategoryParams errorCategoryParams)
        {
            var result = await _errorCategoryService.GetAll(errorCategoryParams);
            Response.AddPaginationHeader(new Helper.PaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages));
            _responseDto.Result = result;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetErrorCategories()
        {
            var errorCategories = await _errorCategoryService.GetAll();
            _responseDto.Result = _mapper.Map<List<ErrorCategoryDto>>(errorCategories);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetErrorCategoryById(int id)
        {
            var errorCategory = await _errorCategoryService.GetById(id);
            return Json(_mapper.Map<ErrorCategoryDto>(errorCategory));
        }

        [HttpPost]
        public async Task<IActionResult> AddErrorCategory([FromBody] ErrorCategoryDto errorCategoryDto)
        {

            if (await _errorCategoryService.CheckNameExists(errorCategoryDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên loại lỗi đã tồn tại";
                return Json(_responseDto);
            }

            _errorCategoryService.Add(_mapper.Map<ErrorCategory>(errorCategoryDto));
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
        public async Task<IActionResult> UpdateErrorCategory([FromBody] ErrorCategoryDto errorCategoryDto)
        {
            var errorCategory = await _errorCategoryService.GetById(errorCategoryDto.Id);
            if (errorCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'loại lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _errorCategoryService.CheckNameExists(errorCategoryDto.Name) && errorCategoryDto.Name != errorCategory.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên loại lỗi đã tồn tại";
                return Json(_responseDto);
            }

            _errorCategoryService.Update(_mapper.Map(errorCategoryDto, errorCategory));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật loại lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteErrorCategory([FromBody] int id)
        {
            var errorCategory = await _errorCategoryService.GetById(id);
            if (errorCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'loại lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            _errorCategoryService.Delete(errorCategory);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa loại lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddErrorCategoryByNames([FromBody] List<string> names)
        {
            if(!names.Any())
            {
                _responseDto.Message = "Không có loại lỗi mới nào được thêm";
                return Json(_responseDto);
            }
            var existingErrorCategories = await _errorCategoryService.GetByNames(names);
            var existingNames = existingErrorCategories.Select(x => x.Name).ToList();
            var newNames = names.Except(existingNames).ToList();
            foreach (var name in newNames)
            {
                var newErrorCategory = new ErrorCategory
                {
                    Name = name,
                    Description = string.Empty
                };
                _errorCategoryService.Add(newErrorCategory);
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
