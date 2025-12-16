using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class OrganizationLibraryController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public OrganizationLibraryController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrganizationTree()
        {
            var tree = await _organizationService.GetOrganizationTree();
            _responseDto.Result = tree;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetOrganizationTreeDropdown()
        {
            var tree = await _organizationService.GetOrganizationTreeDropdown();
            _responseDto.Result = tree;
            return Json(_responseDto);
        }
    }
}
