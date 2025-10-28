using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class FactoryLibraryController : Controller
    {
        private readonly IFactoryService _factoryService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public FactoryLibraryController(IFactoryService factoryService, ISharedService sharedService, IMapper mapper)
        {
            _factoryService = factoryService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFactories()
        {
            var factories = await _factoryService.GetAll();
            return Json(_mapper.Map<List<FactoryDto>>(factories));

        }

        public async Task<IActionResult> GetFactoryById(int id)
        {
            var factory = await _factoryService.GetById(id);
            return Json(_mapper.Map<FactoryDto>(factory));
        }

        [HttpPost]
        public async Task<IActionResult> AddFactory([FromBody] FactoryDto factoryDto)
        {
            if (await _factoryService.CheckNameExists(factoryDto.Name, factoryDto.UnitId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên nhà máy đã tồn tại";
                return Json(_responseDto);
            }

            _factoryService.Add(_mapper.Map<Factory>(factoryDto));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Thêm nhà máy thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình thêm";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFactory([FromBody] FactoryDto factoryDto)
        {
            var factory = await _factoryService.GetById(factoryDto.Id);
            if (factory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'nhà máy' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _factoryService.CheckNameExists(factoryDto.Name, factory.UnitId) && (factoryDto.Name != factory.Name && factoryDto.UnitId != factory.UnitId);

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên nhà máy đã tồn tại trong đơn vị này";
                return Json(_responseDto);
            }

            _factoryService.Update(_mapper.Map(factoryDto, factory));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật nhà máy thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFactory([FromBody] int id)
        {
            var factory = await _factoryService.GetById(id);
            if (factory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'nhà máy' này trong thư viện";
                return Json(_responseDto);
            }

            _factoryService.Delete(factory);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa nhà máy thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }

    }
}
