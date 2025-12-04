using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class TimeFrameColorController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly ITimeFrameColorService _timeFrameColorService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public TimeFrameColorController(ISharedService sharedService, ITimeFrameColorService timeFrameColorService, IMapper mapper)
        {
            _sharedService = sharedService;
            _timeFrameColorService = timeFrameColorService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTimeFrameColors()
        {
            var timeFrameColors = await _timeFrameColorService.GetAll();
            _responseDto.Result = _mapper.Map<List<TimeFrameColorDto>>(timeFrameColors);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetTimeFrameColorById(int id)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(id);
            _responseDto.Result = _mapper.Map<TimeFrameColorDto>(timeFrameColor);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeFrameColor([FromBody] TimeFrameColorDto timeFrameColorDto)
        {
            if (await _timeFrameColorService.CheckExists(timeFrameColorDto.TimeFrameId, timeFrameColorDto.HexCode))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameColorService.Add(_mapper.Map<TimeFrameColor>(timeFrameColorDto));
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
        public async Task<IActionResult> UpdateTimeFrameColor([FromBody] TimeFrameColorDto timeFrameColorDto)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(timeFrameColorDto.Id);
            if (timeFrameColor == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _timeFrameColorService.CheckExists(timeFrameColorDto.TimeFrameId, timeFrameColorDto.HexCode) &&
                (timeFrameColorDto.TimeFrameId != timeFrameColor.TimeFrameId || timeFrameColorDto.HexCode != timeFrameColor.HexCode);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameColorService.Update(_mapper.Map(timeFrameColorDto, timeFrameColor));
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
        public async Task<IActionResult> DeleteTimeFrameColor([FromBody] int id)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(id);
            if (timeFrameColor == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _timeFrameColorService.Delete(timeFrameColor);
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
