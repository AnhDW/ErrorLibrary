using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorDetailAttachmentLibraryController : Controller
    {
        private readonly IErrorDetailAttachmentService _errorDetailAttachmentService;
        private readonly IErrorDetailService _errorDetailService;
        private readonly IFileService _fileService;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorDetailAttachmentLibraryController(IErrorDetailAttachmentService errorDetailAttachmentService, IFileService fileService, ISharedService sharedService, IMapper mapper, IErrorDetailService errorDetailService)
        {
            _errorDetailAttachmentService = errorDetailAttachmentService;
            _fileService = fileService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _errorDetailService = errorDetailService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> GetByErrorDetail(int lineId, int productId, int errorId, string userId)
        {
            var attachments = await _errorDetailAttachmentService.GetByErrorDetail(lineId, productId, errorId, userId);
            _responseDto.Result = _mapper.Map<List<ErrorDetailAttachmentDto>>(attachments);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetErrorDetailAttachmentById(int id)
        {
            var attachment = await _errorDetailAttachmentService.GetById(id);
            _responseDto.Result = _mapper.Map<ErrorDetailAttachmentDto>(attachment);
            return Json(_responseDto);
        }

        [HasPermission("ErrorDetailAttachments", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddErrorDetailAttachment([FromForm] ErrorDetailAttachmentDto errorDetailAttachmentDto)
        {
            var errorDetail = await _errorDetailService.GetById(errorDetailAttachmentDto.LineId, errorDetailAttachmentDto.ProductId, errorDetailAttachmentDto.ErrorId, errorDetailAttachmentDto.UserId);
            if (errorDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chi tiết lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            if (errorDetailAttachmentDto.Files.Count > 0)
            {
                foreach(var file in errorDetailAttachmentDto.Files)
                {
                    var filePath = _fileService.AddAttachment(file);
                    var errorDetailAttachment = new ErrorDetailAttachment
                    {
                        LineId = errorDetailAttachmentDto.LineId,
                        ProductId = errorDetailAttachmentDto.ProductId,
                        ErrorId = errorDetailAttachmentDto.ErrorId,
                        UserId = errorDetailAttachmentDto.UserId,
                        Url = filePath,
                        FileName = file.FileName,
                        ContentType = file.ContentType
                    };
                    errorDetailAttachment.Url = filePath;
                    errorDetailAttachment.FileName = file.FileName;
                    errorDetailAttachment.ContentType = file.ContentType;
                    _errorDetailAttachmentService.Add(errorDetailAttachment);
                }
            }
            if(await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = $"Đã thêm {errorDetailAttachmentDto.Files.Count} hình ảnh thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HasPermission("ErrorDetailAttachments", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteErrorDetailAttachment([FromBody] int id)
        {
            var attachment = await _errorDetailAttachmentService.GetById(id);
            if (attachment == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy hình ảnh";
                return Json(_responseDto);
            }
            _fileService.DeleteAttachment(attachment.Url);
            _errorDetailAttachmentService.Delete(attachment);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa hình ảnh thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }

    }
}
