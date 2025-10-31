using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
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

        public async Task<IActionResult> GetUserOrganizations()
        {
            var userOrganizations = await _userOrganizationService.GetAll();
            return Json(_mapper.Map<List<UserOrganizationDto>>(userOrganizations));
        }

        public async Task<IActionResult> GetUserOrganizationById(string userId, string organizationType, int organizationId)
        {
            var userOrganization = await _userOrganizationService.GetById(userId, organizationType, organizationId);
            return Json(_mapper.Map<UserOrganizationDto>(userOrganization));
        }

        public async Task<IActionResult> GetUserIdsByOrganization(string organizationType, int organizationId)
        {
            var userIds = await _userOrganizationService.GetUserIdsByOrganizationId(organizationType, organizationId);
            return Json(userIds);
        }

        public async Task<IActionResult> GetOrganizationsByUserId(string userId)
        {
            var organizations = await _userOrganizationService.GetOrganizationIdsByUserId(userId);
            
            return Json(organizations.Select(x=>new {x.organizationType, x.organizationId}));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrganizationsByUser([FromBody] UpdateOrganizationsByUserDto updateOrganizationsByUserDto)
        {
            var newOrganizations = updateOrganizationsByUserDto.Organizations.Select(x => (organizationType: x.OrganizationType, organizationId: x.OrganizationId)).ToList();
            var currentOrganizations = await _userOrganizationService.GetOrganizationIdsByUserId(updateOrganizationsByUserDto.UserId);
            var addOrganizations = newOrganizations.Except(currentOrganizations);
            var delOrganizations = currentOrganizations.Except(newOrganizations);

            foreach (var organization in addOrganizations)
            {
                _userOrganizationService.Add(new UserOrganization
                {
                    UserId = updateOrganizationsByUserDto.UserId,
                    OrganizationType = organization.organizationType,
                    OrganizationId = organization.organizationId,
                });
            }

            foreach (var organization in delOrganizations)
            {
                var userOrganization = await _userOrganizationService
                    .GetById(updateOrganizationsByUserDto.UserId, organization.organizationType, organization.organizationId);
                _userOrganizationService.Delete(userOrganization);
            }

            if(await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }

            _responseDto.Message = "Không có thay đổi";
            return Json(_responseDto);
        }
    }
}
