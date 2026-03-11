using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Controllers
{
    public class OrganizationLibraryController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUnitService _unitService;
        private readonly IFactoryService _factoryService;
        private readonly IEnterpriseService _enterpriseService;
        private readonly ILineService _lineService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public OrganizationLibraryController(IOrganizationService organizationService, IMapper mapper, IUnitService unitService, IFactoryService factoryService, IEnterpriseService enterpriseService, ILineService lineService)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _unitService = unitService;
            _factoryService = factoryService;
            _enterpriseService = enterpriseService;
            _lineService = lineService;
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

        public async Task<IActionResult> GetOrganizationsDisplay()
        {
            var organizations = await _organizationService.GetAllOrganizationsDisplay();

            _responseDto.Result = organizations.OrderBy(x=>x.UnitName).ThenBy(x=>x.FactoryName).ThenBy(x=>x.EnterpriseName).ThenBy(x=>x.LineName);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetFactoriesOrganizationsDisplay()
        {
            var organizations = await _organizationService.GetFactoriesOrganizationsDisplay();

            _responseDto.Result = organizations.OrderBy(x => x.UnitName).ThenBy(x => x.FactoryName);
            return Json(_responseDto);
        }

    }
}
