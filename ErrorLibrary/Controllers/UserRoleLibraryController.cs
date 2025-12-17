using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserRoleLibraryController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public UserRoleLibraryController(IUserRoleService userRoleService, IUserService userService, IRoleService roleService, ISharedService sharedService, IMapper mapper)
        {
            _userRoleService = userRoleService;
            _userService = userService;
            _roleService = roleService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUserIdsByRoleId(string roleId)
        {
            var userIds = await _userRoleService.GetUserIdsByRoleId(roleId);
            _responseDto.Result = userIds;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetRoleIdsByUserId(string userId)
        {
            var roleIds = await _userRoleService.GetRoleIdsByUserId(userId);
            _responseDto.Result = roleIds;
            return Json(_responseDto);
        }

        public async Task<IActionResult> UpdateRolesByUser([FromBody] UpdateRolesByUserDto updateRolesByUserDto)
        {
            var roleIds = await _userRoleService.GetRoleIdsByUserId(updateRolesByUserDto.UserId);
            var addRoleIds = updateRolesByUserDto.RoleIds.Except(roleIds).ToList();
            var delRoleIds = roleIds.Except(updateRolesByUserDto.RoleIds).ToList();
            foreach (var roleId in addRoleIds)
            {
                _userRoleService.Add(new IdentityUserRole<string> { UserId = updateRolesByUserDto.UserId, RoleId = roleId });
            }
            foreach (var roleId in delRoleIds)
            {
                var userRole = await _userRoleService.GetById(updateRolesByUserDto.UserId, roleId);
                _userRoleService.Delete(userRole);
            }
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Roles updated successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            return Json(_responseDto);
        }

    }
}
