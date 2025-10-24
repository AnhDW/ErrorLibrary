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
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public UserLibraryController(IUserService userService, IAuthService authService, ISharedService sharedService, IFileService fileService, IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _sharedService = sharedService;
            _fileService = fileService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
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

        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetById(id);
            return Json(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] RegistrationRequestDto userDto)
        {
            if (userDto.File != null)
            {
                userDto.AvatarUrl = await _fileService.AddCompressAttachment(userDto.File);
            }
            var register = await _authService.Register(userDto);
            if (register.Error)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Lỗi trong quá trình thêm";
                return Json(_responseDto);
            }
            //Decode password
            _responseDto.Message = "Thêm thành công";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm] UserDto userDto)
        {
            var user = await _userService.GetById(userDto.Id);
            if (user == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Tài khoản' này trong thư viện";
                return Json(_responseDto);
            }
            if (userDto.File != null)
            {
                _fileService.DeleteAttachment(userDto.AvatarUrl);
                userDto.AvatarUrl = await _fileService.AddCompressAttachment(userDto.File);
            }
            _userService.Update(_mapper.Map(userDto, user));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Tài khoản' này trong thư viện";
                return Json(_responseDto);
            }
            _userService.Delete(user);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
