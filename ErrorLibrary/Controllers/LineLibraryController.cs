using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class LineLibraryController : Controller
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly ILineService _lineService;
        private readonly IMapper _mapper;
        private readonly ISharedService _sharedService;
        protected ResponseDto _responseDto;

        public LineLibraryController(IEnterpriseService enterpriseService, IMapper mapper, ILineService lineService, ISharedService sharedService)
        {
            _enterpriseService = enterpriseService;
            _lineService = lineService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _sharedService = sharedService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetLines()
        {
            var lines = await _lineService.GetAll();
            return Json(_mapper.Map<List<LineDto>>(lines
                .OrderBy(x=>x.Enterprise.Factory.Unit.Name)
                .ThenBy(x=>x.Enterprise.Factory.Name)
                .ThenBy(x=>x.Enterprise.Name)
                .ThenBy(x=>x.Name)
                ));
        }

        public async Task<IActionResult> GetLineById(int id)
        {
            var line = await _lineService.GetById(id);
            return Json(_mapper.Map<LineDto>(line));
        }

        [HttpPost]
        public async Task<IActionResult> AddLine([FromBody] LineDto lineDto)
        {
            if (await _lineService.CheckNameExists(lineDto.Name, lineDto.EnterpriseId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên chuyền đã tồn tại trong xưởng này";
                return Json(_responseDto);
            }
            _lineService.Add(_mapper.Map<Line>(lineDto));
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
        public async Task<IActionResult> UpdateLine([FromBody] LineDto lineDto)
        {
            var line = await _lineService.GetById(lineDto.Id);
            if (line == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Chuyền' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _lineService.CheckNameExists(lineDto.Name, lineDto.EnterpriseId) &&
                (lineDto.Name != line.Name || lineDto.EnterpriseId != line.EnterpriseId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên chuyền đã tồn tại trong xưởng này";
                return Json(_responseDto);
            }

            _lineService.Update(_mapper.Map(lineDto, line));
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
        public async Task<IActionResult> DeleteLine([FromBody] int id)
        {
            var line = await _lineService.GetById(id);
            if (line == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Chuyền' này trong thư viện";
                return Json(_responseDto);
            }
            _lineService.Delete(line);
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
