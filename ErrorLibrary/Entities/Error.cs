namespace ErrorLibrary.Entities
{
    public class Error
    {
        public int Id { get; set; }
        public int ErrorGroupId { get; set; }
        public int ProductCategoryId { get; set; }
        public int? ErrorCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ErrorGroup ErrorGroup { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ErrorCategory? ErrorCategory { get; set; }
        public List<Solution> Solutions { get; set; } = new();
        public List<ErrorDetail> ErrorDetails { get; set; } = new();
        public List<InLineDetail> InLineDetails { get; set; } = new();
        public List<EndLineDetail> EndLineDetails { get; set; } = new();
    }
}
