using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class EndLineDetailLibraryController : Controller
    {
        private readonly IEndLineDetailService _endLineDetailService;
        private readonly IErrorGroupService _errorGroupService;
        private readonly IErrorService _errorService;
        private readonly IUserService _userService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public EndLineDetailLibraryController(IEndLineDetailService endLineDetailService, IErrorGroupService errorGroupService, IErrorService errorService, IUserService userService, ISharedService sharedService, IMapper mapper)
        {
            _endLineDetailService = endLineDetailService;
            _errorGroupService = errorGroupService;
            _errorService = errorService;
            _userService = userService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEndLineDetails()
        {
            var endLineDetails = await _endLineDetailService.GetAll();
            _responseDto.Result = _mapper.Map<List<EndLineDetailDto>>(endLineDetails);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetEndLineDetailsByEndLine(int endLineId)
        {
            var endLineDetails = await _endLineDetailService.GetAll();
            var errorGroups = await _errorGroupService.GetAll();
            var errors = await _errorService.GetAll();
            var users = await _userService.GetAll();
            endLineDetails = endLineDetails.Where(x => x.EndLineId == endLineId).ToList();
            var endLineDetailsDto = _mapper.Map<List<EndLineDetailDisplayDto>>(endLineDetails);
            foreach (var item in endLineDetailsDto)
            {
                var error = errors.FirstOrDefault(x => x.Id == item.ErrorId) ?? new Error();
                var user = users.FirstOrDefault(x => x.Id == item.UserId);
                item.Error = _mapper.Map<ErrorDisplayDto>(error);
                item.Error.ErrorGroup = _mapper.Map<ErrorGroupDto>(errorGroups.FirstOrDefault(x => x.Id == error.ErrorGroupId));
                item.User = _mapper.Map<UserDto>(user);
            }
            _responseDto.Result = endLineDetailsDto;
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetEndLineDetailById(int id)
        {
            var endLineDetail = await _endLineDetailService.GetById(id);
            _responseDto.Result = _mapper.Map<EndLineDetailDto>(endLineDetail);
            return Json(_responseDto);
        }

        [HasPermission("EndLineDetails", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddEndLineDetail([FromBody] EndLineDetailDto endLineDetailDto)
        {
            if (await _endLineDetailService.CheckExists(endLineDetailDto.EndLineId, endLineDetailDto.ErrorId, endLineDetailDto.UserId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Đã tồn tại";
                return Json(_responseDto);
            }

            _endLineDetailService.Add(_mapper.Map<EndLineDetail>(endLineDetailDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HasPermission("EndLineDetails", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateEndLineDetail([FromBody] EndLineDetailDto endLineDetailDto)
        {
            var endLineDetail = await _endLineDetailService.GetById(endLineDetailDto.Id);
            if (endLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chi tiết end line' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _endLineDetailService.CheckExists(endLineDetailDto.EndLineId, endLineDetailDto.ErrorId, endLineDetailDto.UserId) && 
                (endLineDetailDto.EndLineId != endLineDetail.EndLineId || endLineDetailDto.ErrorId != endLineDetail.ErrorId || endLineDetailDto.UserId != endLineDetail.UserId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên chi tiết end line đã tồn tại";
                return Json(_responseDto);
            }

            _endLineDetailService.Update(_mapper.Map(endLineDetailDto, endLineDetail));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật chi tiết end line thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HasPermission("EndLineDetails", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteEndLineDetail([FromBody] int id)
        {
            var endLineDetail = await _endLineDetailService.GetById(id);
            if (endLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chi tiết end line' này trong thư viện";
                return Json(_responseDto);
            }

            _endLineDetailService.Delete(endLineDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa chi tiết end line thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
