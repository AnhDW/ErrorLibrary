namespace ErrorLibrary.Helper.EntityParams
{
    public class ErrorParams : PaginationParams
    {
        public List<int> ErrorGroupIds { get; set; } = new List<int>();
        public List<int> ErrorCategoryIds { get; set; } = new List<int>();
        public List<int> ProductCategoryIds { get; set; } = new List<int>();
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CodeName { get; set; }
    }
}
