using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class RoleLibraryController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public RoleLibraryController(IRoleService roleService, ISharedService sharedService, IMapper mapper)
        {
            _roleService = roleService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HasPermission("Roles", "View")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAll();
            _responseDto.Result = _mapper.Map<List<RoleDto>>(roles);
            return Json(_responseDto);
        }

        [HasPermission("Roles", "View")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetById(id);
            _responseDto.Result = _mapper.Map<RoleDto>(role);
            return Json(_responseDto);
        }

        [HasPermission("Roles", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            if (await _roleService.CheckNameExists(roleDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên vai trò đã tồn tại";
                return Json(_responseDto);
            }
            roleDto.Id = Guid.NewGuid().ToString();
            roleDto.NormalizedName = roleDto.Name.ToUpper();
            _roleService.Add(_mapper.Map<ApplicationRole>(roleDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm vai trò thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HasPermission("Roles", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto)
        {
            var role = await _roleService.GetById(roleDto.Id);
            if (role == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'vai trò' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _roleService.CheckNameExists(roleDto.Name) && roleDto.Name != role.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên vai trò đã tồn tại";
                return Json(_responseDto);
            }
            roleDto.NormalizedName = roleDto.Name.ToUpper();
            _roleService.Update(_mapper.Map(roleDto, role));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật vai trò thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HasPermission("Roles", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromBody] string id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'vai trò' này trong thư viện";
                return Json(_responseDto);
            }

            _roleService.Delete(role);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa vai trò thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
