using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string PO { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? File { get; set; }

        public ProductCategoryDto? ProductCategory { get; set; }
    }
}
