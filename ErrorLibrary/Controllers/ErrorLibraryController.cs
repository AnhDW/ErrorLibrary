using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class ErrorLibraryController:Controller
    {
        private readonly IErrorService _errorService;
        private readonly IErrorGroupService _errorGroupService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;
        public ErrorLibraryController(IErrorService errorService, IMapper mapper, IErrorGroupService errorGroupService, ISharedService sharedService)
        {
            _errorService = errorService;
            _mapper = mapper;
            _errorGroupService = errorGroupService;
            _responseDto = new ResponseDto();
            _sharedService = sharedService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetErrorGroups()
        {
            var errorGroups = await _errorGroupService.GetAll();
            return Json(_mapper.Map<List<ErrorGroupDto>>(errorGroups));
        }

        public async Task<IActionResult> GetErrors()
        {
            var errors = await _errorService.GetAll();
            return Json(_mapper.Map<List<ErrorDto>>(errors));
        }

        public async Task<IActionResult> GetErrorById(int id)
        {
            var error = await _errorService.GetById(id);
            return Json(_mapper.Map<ErrorDto>(error));
        }

        [HttpPost]
        public async Task<IActionResult> AddError([FromBody] ErrorDto errorDto)
        {
            _errorService.Add(_mapper.Map<Error>(errorDto));
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
        public async Task<IActionResult> UpdateError([FromBody] ErrorDto errorDto)
        {
            var error = await _errorService.GetById(errorDto.Id);
            if (error == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            _errorService.Update(_mapper.Map(errorDto, error));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteError([FromBody] int id)
        {
            var error = await _errorService.GetById(id);
            if (error == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Lỗi' này trong thư viện";
                return Json(_responseDto);
            }
            _errorService.Delete(error);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
