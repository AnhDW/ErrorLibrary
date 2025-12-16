using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class PermissionLibraryController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public PermissionLibraryController(IPermissionService permissionService, ISharedService sharedService, IMapper mapper)
        {
            _permissionService = permissionService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HasPermission("Permissions", "View")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionService.GetAll();
            _responseDto.Result = _mapper.Map<List<PermissionDto>>(permissions);
            return Json(_responseDto);
        }

        [HasPermission("Permissions", "View")]
        public async Task<IActionResult> GetTreePermissions()
        {
            var permissions = await _permissionService.GetPermissionTreeAsync();
            _responseDto.Result = permissions;
            return Json(_responseDto);
        }

        [HasPermission("Permissions", "View")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _permissionService.GetById(id);
            _responseDto.Result = _mapper.Map<PermissionDto>(permission);
            return Json(_responseDto);
        }

        [HasPermission("Permissions", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody] PermissionDto permissionDto)
        {
            _permissionService.Add(_mapper.Map<Permission>(permissionDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HasPermission("Permissions", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionDto permissionDto)
        {
            var permission = await _permissionService.GetById(permissionDto.Id);
            if (permission == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _permissionService.Update(_mapper.Map(permissionDto, permission));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HasPermission("Permissions", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePermission([FromBody] int id)
        {
            var permission = await _permissionService.GetById(id);
            if (permission == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _permissionService.Delete(permission);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }


    }
}
