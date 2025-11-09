using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Extensions;
using ErrorLibrary.Services.IServices;
using ErrorLibrary.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class ErrorLibraryController:Controller
    {
        private readonly IHubContext<ErrorHub> _hubContext;
        private readonly IErrorService _errorService;
        private readonly IErrorGroupService _errorGroupService;
        private readonly ISharedService _sharedService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorLibraryController(IHubContext<ErrorHub> hubContext, IErrorService errorService, IErrorGroupService errorGroupService, ISharedService sharedService, IMapper mapper, IUserService userService)
        {
            _hubContext = hubContext;
            _errorService = errorService;
            _errorGroupService = errorGroupService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _userService = userService;
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetErrors()
        {
            var errors = await _errorService.GetAll();
            return Json(_mapper.Map<List<ErrorDisplayDto>>(errors));
        }

        public async Task<IActionResult> GetErrorById(int id)
        {
            var error = await _errorService.GetById(id);
            return Json(_mapper.Map<ErrorDisplayDto>(error));
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddError([FromBody] ErrorDto errorDto)
        {
            var user = await _userService.GetById(User.GetUserId());
            var errorGroup =  await _errorGroupService.GetById(errorDto.ErrorGroupId);
            if (errorGroup == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Nhóm Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            var error = _mapper.Map<Error>(errorDto);
            _errorService.Add(error);
            if (await _sharedService.SaveAllChanges())
            {
                var errorDisplayDto = _mapper.Map<ErrorDisplayDto>(error);
                errorDisplayDto.ErrorGroup = _mapper.Map<ErrorGroupDto>(errorGroup);
                await _hubContext.Clients.All.SendAsync("ErrorAdded", errorDisplayDto);
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa thêm dòng 'error' có id:{error.Id}");
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
            if (errorGroup == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Nhóm Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            var error = await _errorService.GetById(errorDto.Id);
            if (error == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            _errorService.Update(_mapper.Map(errorDto, error));
            if (await _sharedService.SaveAllChanges())
            {
                var errorDisplayDto = _mapper.Map<ErrorDisplayDto>(error);
                errorDisplayDto.ErrorGroup = _mapper.Map<ErrorGroupDto>(errorGroup);
                await _hubContext.Clients.All.SendAsync("ErrorUpdated", errorDisplayDto);
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa cập nhật dòng 'error' có id:{error.Id}");
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
                await _hubContext.Clients.All.SendAsync("Notification", $"{user.FullName} vừa xóa dòng 'error' có id:{error.Id}");
                _responseDto.Message = "Xóa thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
