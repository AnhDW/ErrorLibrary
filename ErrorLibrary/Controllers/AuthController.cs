using AutoMapper;
using ErrorLibrary.Authorization.Constants;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        public IActionResult TestAuth()
        {
            return Ok(new
            {
                isAuth = User.Identity!.IsAuthenticated,
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                roles = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value),
                permissions = User.Claims
                    .Where(c => c.Type == PermissionClaimTypes.Permission)
                    .Select(c => c.Value)
            });
        }

    }
}
