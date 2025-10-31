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
        private readonly IErrorDetailService _errorDetailService;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorDetailLibraryController(IErrorDetailService errorDetailService, ISharedService sharedService, IMapper mapper)
        {
            _errorDetailService = errorDetailService;
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
        public async Task<IActionResult> AddErrorDetail([FromBody] ErrorDetailDto errorDetailDto)
        {
            errorDetailDto.UserId = User.GetUserId();
            _errorDetailService.Add(_mapper.Map<ErrorDetail>(errorDetailDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm chi tiết lỗi thành công";
                return Json(_responseDto);
            }

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
                _responseDto.Message = "Xóa chi tiết lỗi thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
