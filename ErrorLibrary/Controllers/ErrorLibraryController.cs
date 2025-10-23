using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class ErrorLibraryController:Controller
    {
        private readonly IErrorService _errorService;
        private readonly IErrorGroupService _errorGroupService;
        private readonly IMapper _mapper;
        public ErrorLibraryController(IErrorService errorService, IMapper mapper, IErrorGroupService errorGroupService)
        {
            _errorService = errorService;
            _mapper = mapper;
            _errorGroupService = errorGroupService;
        }

        public async Task<IActionResult> Index()
        {
            var errors = await _errorService.GetAll();

            return View(_mapper.Map<List<ErrorDto>>(errors));
        }

        public async Task<JsonResult> GetErrors()
        {
            var errors = await _errorService.GetAll();
            return Json(_mapper.Map<List<ErrorDto>>(errors));
        }

        public async Task<JsonResult> GetErrorGroups()
        {
            var errorGroups = await _errorGroupService.GetAll();
            return Json(_mapper.Map<List<ErrorGroupDto>>(errorGroups));
        }
    }
}
