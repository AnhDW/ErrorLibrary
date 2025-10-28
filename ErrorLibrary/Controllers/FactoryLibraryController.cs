using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class FactoryLibraryController : Controller
    {
        private readonly IFactoryService _factoryService;
        private readonly IUnitService _unitService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public FactoryLibraryController(IUnitService unitService, IFactoryService factoryService, ISharedService sharedService, IMapper mapper)
        {
            _unitService = unitService;
            _factoryService = factoryService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
