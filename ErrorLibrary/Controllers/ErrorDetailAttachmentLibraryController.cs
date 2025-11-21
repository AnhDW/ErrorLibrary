using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorDetailAttachmentLibraryController : Controller
    {
        private readonly IErrorDetailAttachmentService _errorDetailAttachmentService;
        private readonly IFileService _fileService;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorDetailAttachmentLibraryController(IErrorDetailAttachmentService errorDetailAttachmentService, IFileService fileService, ISharedService sharedService, IMapper mapper)
        {
            _errorDetailAttachmentService = errorDetailAttachmentService;
            _fileService = fileService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
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

        [HttpPost]
        public async Task<IActionResult> AddErrorDetailAttachment([FromForm] ErrorDetailAttachmentDto errorDetailAttachmentDto)
        {
            var filePath = _fileService.AddAttachment(errorDetailAttachmentDto.File);
            errorDetailAttachmentDto.Url = filePath;
            errorDetailAttachmentDto.FileName = errorDetailAttachmentDto.File.FileName;
            errorDetailAttachmentDto.ContentType = errorDetailAttachmentDto.File.ContentType;
            _errorDetailAttachmentService.Add(_mapper.Map<ErrorDetailAttachment>(errorDetailAttachmentDto));
            if(await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm hình ảnh thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteErrorDetailAttachment(int id)
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
