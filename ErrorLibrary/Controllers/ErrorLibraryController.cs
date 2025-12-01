using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Extensions;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using ErrorLibrary.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using ProductCategoryLibrary.Services.IServices;
using SixLabors.ImageSharp.ColorSpaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class ErrorLibraryController:Controller
    {
        private readonly IHubContext<ErrorHub> _hubContext;
        private readonly IErrorService _errorService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IErrorGroupService _errorGroupService;
        private readonly IErrorCategoryService _errorCategoryService;
        private readonly ISharedService _sharedService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorLibraryController(IHubContext<ErrorHub> hubContext, IErrorService errorService, IErrorGroupService errorGroupService, ISharedService sharedService, IMapper mapper, IUserService userService, IProductCategoryService productCategoryService, IErrorCategoryService errorCategoryService)
        {
            _hubContext = hubContext;
            _errorService = errorService;
            _errorGroupService = errorGroupService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _userService = userService;
            _productCategoryService = productCategoryService;
            _errorCategoryService = errorCategoryService;
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ErrorExcelPreview([FromBody] PreviewErrorExcelDto previewErrorExcelDto)
        {
            return PartialView("ErrorExcelPreview", previewErrorExcelDto);
        }

        public async Task<IActionResult> GetErrorsPagination([FromQuery] ErrorParams errorParams)
        {
            var result = await _errorService.GetAll(errorParams);
            Response.AddPaginationHeader(new Helper.PaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages));
            _responseDto.Result = result;
            return Json(_responseDto);
        }

        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetErrors()
        {
            var errors = await _errorService.GetAll();
            _responseDto.Result = _mapper.Map<List<ErrorDisplayDto>>(
                errors.OrderBy(x => Regex.Match(x.Code, @"^[A-Za-z]+").Value)
                .ThenBy(x => int.Parse(Regex.Match(x.Code, @"\d+").Value)));
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetErrorById(int id)
        {
            var error = await _errorService.GetById(id);
            return Json(_mapper.Map<ErrorDisplayDto>(error));
        }

        public async Task<IActionResult> GenerateErrorCode(int errorGroupId)
        {
            var errorGroup = await _errorGroupService.GetById(errorGroupId);
            var existingCodes = await _errorService.GetAllCodesByErrorGroupId(errorGroup.Id);
            var nextCode = _errorService.GetNextErrorCode(errorGroup.Code, existingCodes);
            _responseDto.Result = nextCode;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GenerateErrorCodeWhenUpdate(int errorGroupId, string currentCode)
        {
            var errorGroup = await _errorGroupService.GetById(errorGroupId);
            var existingCodes = await _errorService.GetAllCodesByErrorGroupId(errorGroup.Id);
            existingCodes = existingCodes.Where(x => x != currentCode).ToList();
            var nextCode = _errorService.GetNextErrorCode(errorGroup.Code, existingCodes);
            _responseDto.Result = nextCode;
            return Json(_responseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddError([FromBody] ErrorDto errorDto)
        {
            var user = await _userService.GetById(User.GetUserId());
            var errorGroup =  await _errorGroupService.GetById(errorDto.ErrorGroupId);
            var errorCategory = await _errorCategoryService.GetById(errorDto.ErrorCategoryId ?? -1);
            var productCategory = await _productCategoryService.GetById(errorDto.ProductCategoryId);
            if (errorGroup == null || productCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorGroup == null ? "Không tìm thấy 'Nhóm Lỗi' này trong thư viện" : "Không tìm thấy 'Chủng loại sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }

            if (await _errorService.CheckNameExists(errorDto.ErrorGroupId, errorDto.ErrorCategoryId ?? -1, errorDto.ProductCategoryId, errorDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên lỗi đã tồn tại";
                return Json(_responseDto);
            }

            if (await _errorService.CheckCodeExists(errorDto.Code))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Mã lỗi đã tồn tại";
                return Json(_responseDto);
            }
            
            var error = _mapper.Map<Error>(errorDto);
            _errorService.Add(error);
            if (await _sharedService.SaveAllChanges())
            {
                var errorDisplayDto = _mapper.Map<ErrorDisplayDto>(error);
                errorDisplayDto.ErrorGroup = _mapper.Map<ErrorGroupDto>(errorGroup);
                errorDisplayDto.ErrorCategory = _mapper.Map<ErrorCategoryDto>(errorCategory);
                errorDisplayDto.ProductCategory = _mapper.Map<ProductCategoryDto>(productCategory);
                await _hubContext.Clients.All.SendAsync("ErrorAdded", errorDisplayDto);
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa thêm dòng 'error':{error.Code}");
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateError([FromBody] ErrorDto errorDto)
        {
            var user = await _userService.GetById(User.GetUserId());
            var errorGroup = await _errorGroupService.GetById(errorDto.ErrorGroupId);
            var errorCategory = await _errorCategoryService.GetById(errorDto.ErrorCategoryId ?? -1);
            var productCategory = await _productCategoryService.GetById(errorDto.ProductCategoryId);
            if (errorGroup == null || productCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorGroup == null ? "Không tìm thấy 'Nhóm Lỗi' này trong thư viện" : "Không tìm thấy 'Chủng loại sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }
            var error = await _errorService.GetById(errorDto.Id);
            if (error == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _errorService.CheckNameExists(errorDto.ErrorGroupId, errorDto.ErrorCategoryId ?? -1, errorDto.ProductCategoryId, errorDto.Name) 
                && errorDto.Name != error.Name;
            bool isCodeExists = await _errorService.CheckCodeExists(errorDto.Code) && errorDto.Code != error.Code;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên lỗi đã tồn tại";
                return Json(_responseDto);
            }

            if (isCodeExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Mã lỗi đã tồn tại";
                return Json(_responseDto);
            }

            _errorService.Update(_mapper.Map(errorDto, error));
            if (await _sharedService.SaveAllChanges())
            {
                var errorDisplayDto = _mapper.Map<ErrorDisplayDto>(error);
                errorDisplayDto.ErrorGroup = _mapper.Map<ErrorGroupDto>(errorGroup);
                errorDisplayDto.ErrorCategory = _mapper.Map<ErrorCategoryDto>(errorCategory);
                errorDisplayDto.ProductCategory = _mapper.Map<ProductCategoryDto>(productCategory);
                await _hubContext.Clients.All.SendAsync("ErrorUpdated", errorDisplayDto);
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa cập nhật dòng 'error':{error.Code}");
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteError([FromBody] int id)
        {
            var user = await _userService.GetById(User.GetUserId());
            var error = await _errorService.GetById(id);
            if (error == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            _errorService.Delete(error);
            if (await _sharedService.SaveAllChanges())
            {
                await _hubContext.Clients.All.SendAsync("ErrorDeleted", id);
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa xóa dòng 'error':{error.Code}");
                _responseDto.Message = "Xóa thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> ImportErrorsToExcel([FromForm] ImportErrorDto importErrorDto)
        {
            ExcelPackage.License.SetNonCommercialPersonal("ErrorLibrary");

            var errorExcelDtos = new List<ErrorExcelDto>();
            using (var stream = new MemoryStream())
            {
                await importErrorDto.File.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[importErrorDto.WorksheetIndex];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Bỏ header
                    {
                        errorExcelDtos.Add(new ErrorExcelDto
                        {
                            ErrorGroup = worksheet.Cells[row, 1].Text,
                            ProductCategory = worksheet.Cells[row, 2].Text,
                            ErrorName = worksheet.Cells[row, 3].Text,
                            ErrorCategory = worksheet.Cells[row, 4].Text,
                        });
                    }
                }
            }
            var errorGroupNames = errorExcelDtos.Select(x => x.ErrorGroup).Distinct().ToList();
            var productCategoryNames = errorExcelDtos.Select(x => x.ProductCategory).Distinct().ToList();
            var errorCategoryNames = errorExcelDtos.Select(x => x.ErrorCategory).Distinct().ToList();

            var errorGroups = await _errorGroupService.GetByNames(errorGroupNames);
            var productCategories = await _productCategoryService.GetByNames(productCategoryNames);
            var errorCategories = await _errorCategoryService.GetByNames(errorCategoryNames);

            var errorGroupNamesExcept = errorGroupNames.Except(errorGroups.Select(x => x.Name)).ToList();
            var productCategoryNamesExcept = productCategoryNames.Except(productCategories.Select(x => x.Name)).ToList();
            var errorCategoryNamesExcept = errorCategoryNames.Except(errorCategories.Select(x => x.Name)).ToList();

            var previewErrorExcel = new PreviewErrorExcelDto
            {
                ErrorGroups = _mapper.Map<List<ErrorGroupDto>>(errorGroups),
                ProductCategories = _mapper.Map<List<ProductCategoryDto>>(productCategories),
                ErrorCategories = _mapper.Map<List<ErrorCategoryDto>>(errorCategories),
                ErrorGroupNamesExcept = errorGroupNamesExcept,
                ProductCategoryNamesExcept = productCategoryNamesExcept,
                ErrorCategoryNamesExcept = errorCategoryNamesExcept,
                Excel = errorExcelDtos,
            };
            _responseDto.Result = previewErrorExcel;
            return Json(_responseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddErrorsToErrorExcelDto([FromBody] List<ErrorExcelDto> errorExcelDtos)
        {
            var user = await _userService.GetById(User.GetUserId());
            var errorGroups = await _errorGroupService.GetByNames(errorExcelDtos.Select(x => x.ErrorGroup).Distinct().ToList());
            var errorCategories = await _errorCategoryService.GetByNames(errorExcelDtos.Select(x => x.ErrorCategory).Distinct().ToList());
            var productCategories = await _productCategoryService.GetByNames(errorExcelDtos.Select(x => x.ProductCategory).Distinct().ToList());
            var existingErrors = await _errorService.GetAll();
            List<Error> errors = new();
            var existingNames = new HashSet<string>(
                existingErrors.Select(x => $"{x.ErrorGroupId}|{x.ErrorCategoryId}|{x.ProductCategoryId}|{x.Name}")
            );
            foreach (var errorGroup in errorGroups)
            {
                var existingErrorCodes = existingErrors.Where(x => x.ErrorGroupId == errorGroup.Id).Select(x => x.Code).ToList();
                var errorExcelDtosByErrorGroup = errorExcelDtos.Where(x => x.ErrorGroup == errorGroup.Name);
                foreach (var errorExcelDto in errorExcelDtosByErrorGroup)
                {
                    var productCategoryId = productCategories.First(x => x.Name == errorExcelDto.ProductCategory).Id;
                    var errorCategoryId = errorCategories.First(x => x.Name == errorExcelDto.ErrorCategory).Id;
                    string key = $"{errorGroup.Id}|{errorCategoryId}|{productCategoryId}|{errorExcelDto.ErrorName}";
                    
                    if (existingNames.Contains(key)) continue;

                    var code = _errorService.GetNextErrorCode(errorGroup.Code, existingErrorCodes);
                    var error = new Error
                    {
                        Name = errorExcelDto.ErrorName,
                        Code = code,
                        ErrorGroupId = errorGroup.Id,
                        ProductCategoryId = productCategoryId,
                        ErrorCategoryId = errorCategoryId,
                    };
                    existingErrorCodes.Add(code);
                    errors.Add(error);
                }
            }
            _errorService.AddRange(errors);
            if (await _sharedService.SaveAllChanges())
            {
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa thêm nhiều dòng 'error' từ file excel");
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            var errors = await _errorService.GetAll();
            _errorService.DeleteRange(errors);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa tất cả lỗi thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
