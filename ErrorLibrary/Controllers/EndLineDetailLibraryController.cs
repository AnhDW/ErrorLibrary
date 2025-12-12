using AutoMapper;
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
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public EndLineDetailLibraryController(IEndLineDetailService endLineDetailService, ISharedService sharedService, IMapper mapper)
        {
            _endLineDetailService = endLineDetailService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

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

        public async Task<IActionResult> GetEndLineDetailById(int id)
        {
            var endLineDetail = await _endLineDetailService.GetById(id);
            _responseDto.Result = _mapper.Map<EndLineDetailDto>(endLineDetail);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEndLineDetail([FromBody] EndLineDetailDto endLineDetailDto)
        {
            if (await _endLineDetailService.CheckExists(endLineDetailDto.EndLineId, endLineDetailDto.ErrorId, endLineDetailDto.UserId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _endLineDetailService.Add(_mapper.Map<EndLineDetail>(endLineDetailDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEndLineDetail([FromBody] EndLineDetailDto endLineDetailDto)
        {
            var endLineDetail = await _endLineDetailService.GetById(endLineDetailDto.Id);
            if (endLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _endLineDetailService.CheckExists(endLineDetailDto.EndLineId, endLineDetailDto.ErrorId, endLineDetailDto.UserId) && 
                (endLineDetailDto.EndLineId != endLineDetail.EndLineId || endLineDetailDto.ErrorId != endLineDetail.ErrorId || endLineDetailDto.UserId != endLineDetail.UserId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _endLineDetailService.Update(_mapper.Map(endLineDetailDto, endLineDetail));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEndLineDetail([FromBody] int id)
        {
            var endLineDetail = await _endLineDetailService.GetById(id);
            if (endLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _endLineDetailService.Delete(endLineDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa đơn vị thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
