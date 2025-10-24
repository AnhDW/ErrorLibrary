using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        public AuthController(IAuthService authService)
        {
            _responseDto = new ResponseDto();
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto requestDto)
        {
            var loginService = await _authService.Login(requestDto);
            if (loginService.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Sai tên đăng nhập hoặc mật khẩu";
                return Json(_responseDto);
            }
            LoginResponseDto responseDto = new LoginResponseDto();
            responseDto.User = loginService.User;
            responseDto.Token = loginService.Token;
            _responseDto.IsSuccess = true;
            _responseDto.Message = "Thành công";
            _responseDto.Result = responseDto;

            return Json(_responseDto);
        }
    }
}
