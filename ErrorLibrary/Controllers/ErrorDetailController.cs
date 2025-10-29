using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorDetailController : Controller
    {
        private readonly IErrorDetailService _errorDetailService;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        protected ResponseDto _responseDto;

        public ErrorDetailController(IErrorDetailService errorDetailService, ISharedService sharedService, IMapper mapper)
        {
            _errorDetailService = errorDetailService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            return Json(_responseDto);
        }
    }
}
