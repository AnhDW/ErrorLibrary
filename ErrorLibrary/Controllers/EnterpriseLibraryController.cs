using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class EnterpriseLibraryController : Controller
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly IFactoryService _factoryService;
        private readonly IMapper _mapper;
        private readonly ISharedService _sharedService;
        protected ResponseDto _responseDto;

        public EnterpriseLibraryController(IEnterpriseService enterpriseService, IMapper mapper, IFactoryService factoryService, ISharedService sharedService)
        {
            _enterpriseService = enterpriseService;
            _factoryService = factoryService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _sharedService = sharedService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEnterprises()
        {
            var enterprises = await _enterpriseService.GetAll();
            return Json(_mapper.Map<List<EnterpriseDto>>(enterprises.OrderBy(x => x.Factory.UnitId).ThenBy(x => x.FactoryId).ThenBy(x => x.Id)));
        }

        public async Task<IActionResult> GetEnterpriseById(int id)
        {
            var enterprise = await _enterpriseService.GetById(id);
            return Json(_mapper.Map<EnterpriseDto>(enterprise));
        }

        [HttpPost]
        public async Task<IActionResult> AddEnterprise([FromBody] EnterpriseDto enterpriseDto)
        {
            if(await _enterpriseService.CheckNameExists(enterpriseDto.Name, enterpriseDto.FactoryId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đã tồn tại trong nhà máy này";
                return Json(_responseDto);
            }
            _enterpriseService.Add(_mapper.Map<Enterprise>(enterpriseDto));
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
        public async Task<IActionResult> UpdateEnterprise([FromBody] EnterpriseDto enterpriseDto)
        {
            var enterprise = await _enterpriseService.GetById(enterpriseDto.Id);
            if (enterprise == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Xưởng' này trong thư viện";
                return Json(_responseDto);
            }
            
            bool isNameExists = await _enterpriseService.CheckNameExists(enterpriseDto.Name, enterpriseDto.FactoryId) &&
                (enterpriseDto.Name != enterprise.Name || enterpriseDto.FactoryId != enterprise.FactoryId);
            
            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên đã tồn tại trong nhà máy này";
                return Json(_responseDto);
            }

            _enterpriseService.Update(_mapper.Map(enterpriseDto, enterprise));
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
        public async Task<IActionResult> DeleteEnterprise([FromBody] int id)
        {
            var enterprise = await _enterpriseService.GetById(id);
            if (enterprise == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'Xưởng' này trong thư viện";
                return Json(_responseDto);
            }
            _enterpriseService.Delete(enterprise);
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


