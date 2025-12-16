using AutoMapper;
using ErrorLibrary.Authorization.Attributes;
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

        [HasPermission("Products", "View")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAll();
            _responseDto.Result = _mapper.Map<List<ProductDto>>(products);
            return Json(_responseDto);
        }

        [HasPermission("Products", "View")]
        public async Task<IActionResult> GetProductsByProductCategoryById(int productCategoryId)
        {
            var products = await _productService.GetAllByProductCategoryId(productCategoryId);
            _responseDto.Result = _mapper.Map<List<ProductDto>>(products);
            return Json(_responseDto);
        }

        [HasPermission("Products", "View")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetById(id);
            _responseDto.Result = _mapper.Map<ProductDto>(product);
            return Json(_responseDto);
        }

        [HasPermission("Products", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            if (productDto.FrontFile != null)
            {
                productDto.FrontImageUrl = await _fileService.AddCompressAttachment(productDto.FrontFile);
            }
            if (productDto.BackFile != null)
            {
                productDto.BackImageUrl = await _fileService.AddCompressAttachment(productDto.BackFile);
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

        [HasPermission("Products", "Update")]
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
            if (productDto.FrontFile != null)
            {
                productDto.FrontImageUrl = await _fileService.AddCompressAttachment(productDto.FrontFile);
            }
            if (productDto.BackFile != null)
            {
                productDto.BackImageUrl = await _fileService.AddCompressAttachment(productDto.BackFile);
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

        [HasPermission("Products", "Delete")]
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
            _fileService.DeleteAttachment(product.FrontImageUrl);
            _fileService.DeleteAttachment(product.BackImageUrl);
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
