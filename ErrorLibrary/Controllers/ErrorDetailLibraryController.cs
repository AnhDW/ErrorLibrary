using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Extensions;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ErrorLibrary.Controllers
{
    public class ErrorDetailLibraryController : Controller
    {
        private readonly IErrorDetailAttachmentService _errorDetailAttachmentService;
        private readonly IErrorDetailService _errorDetailService;
        private readonly IFileService _fileService;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorDetailLibraryController(IErrorDetailAttachmentService errorDetailAttachmentService, IErrorDetailService errorDetailService, IFileService fileService, ISharedService sharedService, IMapper mapper)
        {
            _errorDetailAttachmentService = errorDetailAttachmentService;
            _errorDetailService = errorDetailService;
            _fileService = fileService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetErrorDetails()
        {
            var errorDetails = await _errorDetailService.GetAll();
            return Json(_mapper.Map<List<ErrorDetailDto>>(errorDetails));
        }

        public async Task<IActionResult> GetErrorDetailById(int lineId, int productId, int errorId, string userId)
        {
            var errorDetail = await _errorDetailService.GetById(lineId, productId, errorId, userId);
            return Json(_mapper.Map<ErrorDetailDto>(errorDetail));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddErrorDetail([FromForm] ErrorDetailDto errorDetailDto)
        {
            errorDetailDto.UserId = User.GetUserId();
            var errorDetail = _mapper.Map<ErrorDetail>(errorDetailDto);
            if (errorDetailDto.Files.Count > 0)
            {
                await AddAttachments(errorDetailDto.Files, errorDetailDto.LineId, errorDetailDto.ProductId, errorDetailDto.ErrorId, errorDetailDto.UserId);
                //errorDetail.ErrorDetailAttachments.Add(new ErrorDetailAttachment
                //{
                //    Url = "temp",
                //    FileName = "temp",
                //    ContentType = "temp",
                //    CreatedAt = DateTime.UtcNow
                //});
            }
            _errorDetailService.Add(errorDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm chi tiết lỗi thành công";
                return Json(_responseDto);
            }

            await DeleteAttachments(errorDetailDto.LineId, errorDetailDto.ProductId, errorDetailDto.ErrorId, errorDetailDto.UserId);
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateErrorDetail([FromBody] ErrorDetailDto errorDetailDto)
        {
            var errorDetail = await _errorDetailService.GetById(errorDetailDto.LineId, errorDetailDto.ProductId, errorDetailDto.ErrorId, errorDetailDto.UserId);
            if (errorDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chi tiết lỗi' này trong thư viện";
                return Json(_responseDto);
            }

            if(errorDetailDto.UserId != User.GetUserId())
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không thể cập nhật dòng của người khác";
                return Json(_responseDto);
            }

            _errorDetailService.Update(_mapper.Map(errorDetailDto, errorDetail));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật chi tiết lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteErrorDetail([FromBody] DeleteErrorDetailDto deleteErrorDetailDto)
        {
            var errorDetail = await _errorDetailService.GetById(deleteErrorDetailDto.LineId, deleteErrorDetailDto.ProductId, deleteErrorDetailDto.ErrorId, deleteErrorDetailDto.UserId);
            if (errorDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chi tiết lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            if (deleteErrorDetailDto.UserId != User.GetUserId())
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không thể xóa dòng của người khác";
                return Json(_responseDto);
            }
            _errorDetailService.Delete(errorDetail);
            if (await _sharedService.SaveAllChanges())
            {
                await DeleteAttachments(deleteErrorDetailDto.LineId, deleteErrorDetailDto.ProductId, deleteErrorDetailDto.ErrorId, deleteErrorDetailDto.UserId);
                _responseDto.Message = "Xóa chi tiết lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }

        private async Task<bool> AddAttachments(List<IFormFile> files, int lineId, int productId, int errorId, string userId)
        {
            foreach (var file in files)
            {
                var filePath = _fileService.AddAttachment(file);
                if (string.IsNullOrEmpty(filePath))
                {
                    return false;
                }
                var errorDetailAttachment = new ErrorDetailAttachment
                {
                    LineId = lineId,
                    ProductId = productId,
                    ErrorId = errorId,
                    UserId = userId,
                    Url = filePath,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    CreatedAt = DateTime.UtcNow
                };
                _errorDetailAttachmentService.Add(errorDetailAttachment);

            }
            return await _sharedService.SaveAllChanges();
        }

        private async Task<bool> DeleteAttachments(int lineId, int productId, int errorId, string userId)
        {
            var attachments = await _errorDetailAttachmentService.GetByErrorDetail(lineId, productId, errorId, userId);
            foreach (var attachment in attachments)
            {
                _fileService.DeleteAttachment(attachment.Url);
                _errorDetailAttachmentService.Delete(attachment);
            }
            return await _sharedService.SaveAllChanges();
        }
    }
}
