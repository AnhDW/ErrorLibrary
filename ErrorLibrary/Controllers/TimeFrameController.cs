using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class TimeFrameController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly ITimeFrameService _timeFrameService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public TimeFrameController(ISharedService sharedService, ITimeFrameService timeFrameService, IMapper mapper)
        {
            _sharedService = sharedService;
            _timeFrameService = timeFrameService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetTimeFrames()
        {
            var timeFrames = await _timeFrameService.GetAll();
            _responseDto.Result = _mapper.Map<List<TimeFrameDto>>(timeFrames);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetTimeFrameById(int id)
        {
            var timeFrame = await _timeFrameService.GetById(id);
            _responseDto.Result = _mapper.Map<TimeFrameDto>(timeFrame);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeFrame([FromBody] TimeFrameDto timeFrameDto)
        {
            if (await _timeFrameService.CheckNameExists(timeFrameDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameService.Add(_mapper.Map<TimeFrame>(timeFrameDto));
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
        public async Task<IActionResult> UpdateTimeFrame([FromBody] TimeFrameDto timeFrameDto)
        {
            var timeFrame = await _timeFrameService.GetById(timeFrameDto.Id);
            if (timeFrame == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _timeFrameService.CheckNameExists(timeFrameDto.Name) &&
                timeFrameDto.Name != timeFrame.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _timeFrameService.Update(_mapper.Map(timeFrameDto, timeFrame));
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
        public async Task<IActionResult> DeleteTimeFrame([FromBody] int id)
        {
            var timeFrame = await _timeFrameService.GetById(id);
            if (timeFrame == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _timeFrameService.Delete(timeFrame);
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
