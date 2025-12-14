using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class EndLineLibraryController : Controller
    {
        private readonly IEndLineService _endLineService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public EndLineLibraryController(IEndLineService endLineService, ISharedService sharedService, IMapper mapper)
        {
            _endLineService = endLineService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEndLines()
        {
            var endLines = await _endLineService.GetAll();
            _responseDto.Result = _mapper.Map<List<EndLineDto>>(endLines);
            return Json(_responseDto);
        }

        public async Task<IActionResult> GetEndLineById(int id)
        {
            var endLine = await _endLineService.GetById(id);
            _responseDto.Result = _mapper.Map<EndLineDto>(endLine);
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CheckInitAndUpdateEndLine([FromBody] InitAndUpdateEndLineDto initAndUpdateEndLineDto)
        {
            var endLines = await _endLineService.GetAll();
            var existingKeys = _endLineService.BuildExistingEndLineKeySet(endLines);
            var keyToCheck = _endLineService.CheckNameExistsFast(existingKeys, initAndUpdateEndLineDto.LineId, initAndUpdateEndLineDto.ProductId);
            EndLine endLine;
            if (keyToCheck)
            {
                endLine = endLines.FirstOrDefault(endLines =>
                    endLines.LineId == initAndUpdateEndLineDto.LineId &&
                    endLines.ProductId == initAndUpdateEndLineDto.ProductId) ?? new EndLine();
                // Nếu là load lần đầu thì chỉ trả về thông tin 'In line'
                if (initAndUpdateEndLineDto.FirstLoad)
                {
                    _responseDto.Message = "Lấy thông tin 'End line' cho lần load đầu tiên";
                    _responseDto.Result = _mapper.Map<EndLineDto>(endLine);
                    return Json(_responseDto);
                }
                // Không phải load lần đầu thì cập nhật
                initAndUpdateEndLineDto.Id = endLine.Id;
                _endLineService.Update(_mapper.Map(initAndUpdateEndLineDto, endLine));
                _responseDto.Message = "Cập nhật 'End line' thành công";
            }
            else
            {
                endLine = _mapper.Map<EndLine>(initAndUpdateEndLineDto);
                _endLineService.Add(endLine);
                _responseDto.Message = "Khởi tạo 'End line' thành công";
            }
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Result = _mapper.Map<EndLineDto>(endLine);
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình khởi tạo hoặc cập nhật 'In line'";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEndLine([FromBody] EndLineDto endLineDto)
        {
            if (await _endLineService.CheckExists(endLineDto.LineId, endLineDto.ProductId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _endLineService.Add(_mapper.Map<EndLine>(endLineDto));
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
        public async Task<IActionResult> UpdateEndLine([FromBody] EndLineDto endLineDto)
        {
            var endLine = await _endLineService.GetById(endLineDto.Id);
            if (endLine == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _endLineService.CheckExists(endLineDto.LineId, endLineDto.ProductId) &&
                (endLineDto.LineId != endLine.LineId || endLineDto.ProductId != endLine.ProductId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đơn vị đã tồn tại";
                return Json(_responseDto);
            }

            _endLineService.Update(_mapper.Map(endLineDto, endLine));
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
        public async Task<IActionResult> DeleteEndLine([FromBody] int id)
        {
            var endLine = await _endLineService.GetById(id);
            if (endLine == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'đơn vị' này trong thư viện";
                return Json(_responseDto);
            }

            _endLineService.Delete(endLine);
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
