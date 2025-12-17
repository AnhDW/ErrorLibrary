using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class RolePermissionLibraryController : Controller
    {

        private readonly IRolePermissionService _rolePermissionService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public RolePermissionLibraryController(IRolePermissionService rolePermissionService, IRoleService roleService, IPermissionService permissionService, ISharedService sharedService, IMapper mapper)
        {
            _rolePermissionService = rolePermissionService;
            _roleService = roleService;
            _permissionService = permissionService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HasPermission("RolePermissions", "View")]
        public async Task<IActionResult> GetRoleIdsByPermissionId(int permissionId)
        {
            var roleIds = await _rolePermissionService.GetRoleIdsByPermissionId(permissionId);
            _responseDto.Result = roleIds;
            return Json(_responseDto);
        }

        [HasPermission("RolePermissions", "View")]
        public async Task<IActionResult> GetPermissionIdsByRoleId(string roleId)
        {
            var permissionIds = await _rolePermissionService.GetPermissionIdsByRoleId(roleId);
            _responseDto.Result = permissionIds;
            return Json(_responseDto);
        }

        [HasPermission("RolePermissions", "Update")]
        public async Task<IActionResult> UpdatePermissionsByRole([FromBody] UpdatePermissionsByRoleDto updatePermissionsByRoleDto)
        {
            var permissionIds = await _rolePermissionService.GetPermissionIdsByRoleId(updatePermissionsByRoleDto.RoleId);
            var addPermissionIds = updatePermissionsByRoleDto.PermissionIds.Except(permissionIds).ToList();
            var delPermissionIds = permissionIds.Except(updatePermissionsByRoleDto.PermissionIds).ToList();
            foreach (var permissionId in addPermissionIds)
            {
                _rolePermissionService.Add(new RolePermission { RoleId = updatePermissionsByRoleDto.RoleId, PermissionId = permissionId });
            }
            foreach (var permissionId in delPermissionIds)
            {
                var rolePermission = await _rolePermissionService.GetById(updatePermissionsByRoleDto.RoleId, permissionId);
                if (rolePermission != null)
                {
                    _rolePermissionService.Delete(rolePermission);
                }
            }
            if(await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Permissions updated successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            return Json(_responseDto);
        }

    }
}
