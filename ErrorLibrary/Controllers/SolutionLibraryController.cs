using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class SolutionLibraryController : Controller
    {
        private readonly ISolutionService _solutionService;
        private readonly ISharedService _sharedService;
        private readonly IErrorService _errorService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public SolutionLibraryController(ISolutionService solutionService, ISharedService sharedService, IErrorService errorService, IMapper mapper)
        {
            _solutionService = solutionService;
            _sharedService = sharedService;
            _errorService = errorService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public async Task<IActionResult> Index()
        {
            var solutions = await _solutionService.GetAll();
            return View(_mapper.Map<List<SolutionDto>>(solutions));
        }

        public async Task<IActionResult> GetErrorsForSolution()
        {
            var errors = await _errorService.GetAll();
            return Json(_mapper.Map<List<ErrorDto>>(errors));
        }

        public async Task<IActionResult> GetSolutions()
        {
            var solutions = await _solutionService.GetAll();
            return Json(_mapper.Map<List<SolutionDto>>(solutions));
        }

        public async Task<IActionResult> GetSolutionById(int id)
        {
            var Solution = await _solutionService.GetById(id);
            return Json(_mapper.Map<SolutionDto>(Solution));
        }

        [HttpPost]
        public async Task<IActionResult> AddSolution([FromBody] SolutionDto solutionDto)
        {
            _solutionService.Add(_mapper.Map<Solution>(solutionDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSolution([FromBody] SolutionDto solutionDto)
        {
            var solution = await _solutionService.GetById(solutionDto.Id);
            if (solution == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }
            _solutionService.Update(_mapper.Map(solutionDto, solution));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSolution([FromBody] int id)
        {
            var solution = await _solutionService.GetById(id);
            if (solution == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }
            _solutionService.Delete(solution);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }
    }
}
