namespace ErrorLibrary.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string PO {  get; set; }
        public int Quantity { get; set; }
        public string? FrontImageUrl { get; set; }
        public string? BackImageUrl { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; } = new List<ErrorDetail>();
        public List<InLine> InLines { get; set; } = new();
    }
}
