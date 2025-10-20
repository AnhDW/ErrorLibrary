namespace ErrorLibrary.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string PO {  get; set; }
        public string? ImageUrl { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
