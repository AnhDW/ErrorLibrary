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
        private readonly IMapper _mapper;
        public ErrorLibraryController(IErrorService errorService, IMapper mapper)
        {
            _errorService = errorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var errors = await _errorService.GetAll();

            return View(_mapper.Map<List<ErrorDto>>(errors));
        }
    }
}
