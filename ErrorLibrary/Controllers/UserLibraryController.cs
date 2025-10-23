using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserLibraryController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public UserLibraryController(IUserService userService, IMapper mapper, ISharedService sharedService, IAuthService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _sharedService = sharedService;
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAll();
            return Json(_mapper.Map<List<UserDto>>(users));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegistrationRequestDto userDto)
        {
            var register = await _authService.Register(userDto);
            
            //Decode password
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }
    }
}
