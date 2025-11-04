using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using ErrorLibrary.SignalR;
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
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorLibraryController(IHubContext<ErrorHub> hubContext, IErrorService errorService, IErrorGroupService errorGroupService, ISharedService sharedService, IMapper mapper)
        {
            _hubContext = hubContext;
            _errorService = errorService;
            _errorGroupService = errorGroupService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //public async Task<IActionResult> GetErrorGroups()
        //{
        //    var errorGroups = await _errorGroupService.GetAll();
        //    return Json(_mapper.Map<List<ErrorGroupDto>>(errorGroups));
        //}
        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetErrors()
        {
            var errors = await _errorService.GetAll();
            return Json(_mapper.Map<List<ErrorDto>>(errors));
        }

        public async Task<IActionResult> GetErrorById(int id)
        {
            var error = await _errorService.GetById(id);
            return Json(_mapper.Map<ErrorDto>(error));
        }

        [HttpPost]
        public async Task<IActionResult> AddError([FromBody] ErrorDto errorDto)
        {
            var error = _mapper.Map<Error>(errorDto);
            _errorService.Add(error);
            if (await _sharedService.SaveAllChanges())
            {
                await _hubContext.Clients.All.SendAsync("ErrorAdded", error);
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateError([FromBody] ErrorDto errorDto)
        {
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
                await _hubContext.Clients.All.SendAsync("ErrorUpdated", error);
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteError([FromBody] int id)
        {
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
                _responseDto.Message = "Xóa thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
