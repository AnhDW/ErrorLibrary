using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class InLineDetailLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IInLineDetailService _inLineDetailService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public InLineDetailLibraryController(ISharedService sharedService, IInLineDetailService inLineDetailService, IMapper mapper)
        {
            _sharedService = sharedService;
            _inLineDetailService = inLineDetailService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetInLineDetails()
        {
            var inLineDetails = await _inLineDetailService.GetAll();
            _responseDto.Result = _mapper.Map<List<InLineDetailDto>>(inLineDetails);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetInLineDetailById(int id)
        {
            var inLineDetail = await _inLineDetailService.GetById(id);
            _responseDto.Result = _mapper.Map<InLineDetailDto>(inLineDetail);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddInLineDetail([FromBody] InLineDetailDto inLineDetailDto)
        {
            if (await _inLineDetailService.CheckExists(inLineDetailDto.InLineId, inLineDetailDto.TimeFrameId, inLineDetailDto.ErrorId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _inLineDetailService.Add(_mapper.Map<InLineDetail>(inLineDetailDto));
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
        public async Task<IActionResult> UpdateInLineDetail([FromBody] InLineDetailDto inLineDetailDto)
        {
            var inLineDetail = await _inLineDetailService.GetById(inLineDetailDto.Id);
            if (inLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _inLineDetailService.CheckExists(inLineDetailDto.InLineId, inLineDetailDto.TimeFrameId, inLineDetailDto.ErrorId) &&
                (inLineDetailDto.InLineId != inLineDetail.InLineId || inLineDetailDto.TimeFrameId != inLineDetail.TimeFrameId || inLineDetailDto.ErrorId != inLineDetail.ErrorId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _inLineDetailService.Update(_mapper.Map(inLineDetailDto, inLineDetail));
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
        public async Task<IActionResult> DeleteInLineDetail([FromBody] int id)
        {
            var inLineDetail = await _inLineDetailService.GetById(id);
            if (inLineDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _inLineDetailService.Delete(inLineDetail);
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
