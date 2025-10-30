using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryLibrary.Services.IServices;

namespace ErrorLibrary.Controllers
{
    public class ProductCategoryLibraryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ProductCategoryLibraryController(IProductCategoryService productCategoryService, ISharedService sharedService, IMapper mapper)
        {
            _productCategoryService = productCategoryService;
            _sharedService = sharedService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _productCategoryService.GetAll();
            return Json(_mapper.Map<List<ProductCategoryDto>>(productCategories));
        }

        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var productCategory = await _productCategoryService.GetById(id);
            return Json(_mapper.Map<ProductCategoryDto>(productCategory));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategoryDto productCategoryDto)
        {
            if (await _productCategoryService.CheckNameExists(productCategoryDto.Name))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên chủng loại đã tồn tại";
                return Json(_responseDto);
            }

            _productCategoryService.Add(_mapper.Map<ProductCategory>(productCategoryDto));
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
        public async Task<IActionResult> UpdateProductCategory([FromBody] ProductCategoryDto productCategoryDto)
        {
            var productCategory = await _productCategoryService.GetById(productCategoryDto.Id);
            if (productCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chủng loại' này trong thư viện";
                return Json(_responseDto);
            }

            bool isNameExists = await _productCategoryService.CheckNameExists(productCategoryDto.Name) && productCategoryDto.Name != productCategory.Name;

            if (isNameExists)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Tên chủng loại đã tồn tại";
                return Json(_responseDto);
            }

            _productCategoryService.Update(_mapper.Map(productCategoryDto, productCategory));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật chủng loại thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductCategory([FromBody] int id)
        {
            var productCategory = await _productCategoryService.GetById(id);
            if (productCategory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'chủng loại' này trong thư viện";
                return Json(_responseDto);
            }

            _productCategoryService.Delete(productCategory);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Xóa chủng loại thành công";
                return Json(_responseDto);
            }

            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình xóa";
            return Json(_responseDto);
        }
    }
}
