using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserOrganizationLibraryController : Controller
    {
        private readonly IUserOrganizationService _userOrganizationService;
        private readonly ISharedService _sharedService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public UserOrganizationLibraryController(IUserOrganizationService userOrganizationService, ISharedService sharedService, IUserService userService, IMapper mapper)
        {
            _userOrganizationService = userOrganizationService;
            _sharedService = sharedService;
            _userService = userService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var userOrganizations = await _userOrganizationService.GetAll();
            return Json(_mapper.Map<List<UserOrganizationDto>>(userOrganizations));
        }

        public async Task<IActionResult> GetById(string userId, string organizationType, int organizationId)
        {
            var userOrganization = await _userOrganizationService.GetById(userId, organizationType, organizationId);
            return Json(_mapper.Map<UserOrganizationDto>(userOrganization));
        }

        public async Task<IActionResult> GetUsersByOrganizationId(string organizationType, int organizationId)
        {
            var userIds = await _userOrganizationService.GetUserIdsByOrganizationId(organizationType, organizationId);
            var users = await _userService.GetByUserIds(userIds);
            return Json(_mapper.Map<List<UserDto>>(users));
        }

        public async Task<IActionResult> GetOrganizationsByUserId(string userId)
        {
            var organizations = await _userOrganizationService.GetOrganizationIdsByUserId(userId);
            return Json(organizations);
        }
    }
}
