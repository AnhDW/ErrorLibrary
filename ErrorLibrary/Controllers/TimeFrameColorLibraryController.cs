using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class TimeFrameColorLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly ITimeFrameColorService _timeFrameColorService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public TimeFrameColorLibraryController(ISharedService sharedService, ITimeFrameColorService timeFrameColorService, IMapper mapper)
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

        [HasPermission("TimeFrameColors", "View")]
        public async Task<IActionResult> GetTimeFrameColors()
        {
            var timeFrameColors = await _timeFrameColorService.GetAll();
            _responseDto.Result = _mapper.Map<List<TimeFrameColorDto>>(timeFrameColors);
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "View")]
        public async Task<IActionResult> GetByTimeFrame(int timeFrameId)
        {
            var timeFrameColors = await _timeFrameColorService.GetByTimeFrame(timeFrameId);
            _responseDto.Result = _mapper.Map<List<TimeFrameColorDto>>(timeFrameColors.OrderBy(x => x.MinQuantity).ThenBy(x => x.MaxQuantity));
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "View")]
        public async Task<IActionResult> GetTimeFrameColorByQuantity(int timeFrameId, int quantity)
        {
            var timeFrameColors = await _timeFrameColorService.GetByTimeFrame(timeFrameId);
            if(timeFrameColors.Count == 0)
            {
                _responseDto.Result = new TimeFrameColor();
            }
            var timeFrameColor = timeFrameColors.FirstOrDefault(x => quantity >= x.MinQuantity && quantity < x.MaxQuantity) ?? new TimeFrameColor();
            _responseDto.Result = _mapper.Map<TimeFrameColorDto>(timeFrameColor);
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "View")]
        public async Task<IActionResult> GetTimeFrameColorById(int id)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(id);
            _responseDto.Result = _mapper.Map<TimeFrameColorDto>(timeFrameColor);
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "Create")]
        [HttpPost]
        public async Task<IActionResult> CopyAndPasteColor([FromBody] CopyAndPasteColorDto copyAndPasteColorDto)
        {
            var timeFrameColors = await _timeFrameColorService.GetByIds(copyAndPasteColorDto.TimeFrameColorIds);
            List<TimeFrameColor> copiedColors = new List<TimeFrameColor>();
            foreach (var timeFrameColor in timeFrameColors)
            {
                copiedColors.Add(new TimeFrameColor
                {
                    TimeFrameId = copyAndPasteColorDto.TimeFrameId,
                    HexCode = timeFrameColor.HexCode,
                    MinQuantity = timeFrameColor.MinQuantity,
                    MaxQuantity = timeFrameColor.MaxQuantity
                });
            }

            _timeFrameColorService.AddRange(copiedColors);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Sao chép thành công";
                return Json(_responseDto);
            }
            _responseDto.Message = "Lỗi trong quá trình sao chép";
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddTimeFrameColor([FromBody] TimeFrameColorDto timeFrameColorDto)
        {
            if (await _timeFrameColorService.CheckExists(timeFrameColorDto.TimeFrameId, timeFrameColorDto.HexCode))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên màu đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameColorService.Add(_mapper.Map<TimeFrameColor>(timeFrameColorDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm màu thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateTimeFrameColor([FromBody] TimeFrameColorDto timeFrameColorDto)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(timeFrameColorDto.Id);
            if (timeFrameColor == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'màu' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _timeFrameColorService.CheckExists(timeFrameColorDto.TimeFrameId, timeFrameColorDto.HexCode) &&
                (timeFrameColorDto.TimeFrameId != timeFrameColor.TimeFrameId || timeFrameColorDto.HexCode.ToLower() != timeFrameColor.HexCode.ToLower());

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên màu đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameColorService.Update(_mapper.Map(timeFrameColorDto, timeFrameColor));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật màu thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HasPermission("TimeFrameColors", "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteTimeFrameColor([FromBody] int id)
        {
            var timeFrameColor = await _timeFrameColorService.GetById(id);
            if (timeFrameColor == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'màu' này trong thư viện";
                return Json(_responseDto);
            }

            _timeFrameColorService.Delete(timeFrameColor);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa màu thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
