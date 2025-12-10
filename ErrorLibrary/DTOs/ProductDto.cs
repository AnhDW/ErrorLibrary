using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string PO { get; set; }
        public int Quantity { get; set; }
        public string? FrontImageUrl { get; set; }
        public string? BackImageUrl { get; set; }
        public IFormFile? FrontFile { get; set; }
        public IFormFile? BackFile { get; set; }

        public ProductCategoryDto? ProductCategory { get; set; }
    }
}
