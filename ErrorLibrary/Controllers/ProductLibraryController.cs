using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryLibrary.Services.IServices;
using System.Threading.Tasks;

namespace ErrorLibrary.Controllers
{
    public class ProductLibraryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IFileService _fileService;
        private readonly IProductService _productService;
        private readonly ISharedService _sharedService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ProductLibraryController(IProductCategoryService productCategoryService, IProductService productService, IMapper mapper, ISharedService sharedService, IFileService fileService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _mapper = mapper;
            _sharedService = sharedService;
            _responseDto = new ResponseDto();
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _productCategoryService.GetAll();
            return Json(_mapper.Map<List<ProductCategoryDto>>(productCategories));
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAll();
            return Json(_mapper.Map<List<ProductDto>>(products));
        }

        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetById(id);
            return Json(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            if (productDto.File != null)
            {
                productDto.ImageUrl = await _fileService.AddCompressAttachment(productDto.File);
            }
            _productService.Add(_mapper.Map<Product>(productDto));
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
        public async Task<IActionResult> UpdateProduct([FromForm] ProductDto productDto)
        {
            
            var product = await _productService.GetById(productDto.Id);
            if(product == null)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message = "Không tìm thấy 'sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }
            if (productDto.File != null)
            {
                _fileService.DeleteAttachment(productDto.ImageUrl);
                productDto.ImageUrl = await _fileService.AddCompressAttachment(productDto.File);
            }
            _productService.Update(_mapper.Map(productDto, product));
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct([FromBody] int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Không tìm thấy 'sản phẩm' này trong thư viện";
                return Json(_responseDto);
            }
            _fileService.DeleteAttachment(product.ImageUrl);
            _productService.Delete(product);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Message = "Cập nhật thành công";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Lỗi trong quá trình cập nhật";
            return Json(_responseDto);
        }
    }
}
