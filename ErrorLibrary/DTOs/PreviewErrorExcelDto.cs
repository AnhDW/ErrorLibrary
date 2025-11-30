namespace ErrorLibrary.DTOs
{
    public class PreviewErrorExcelDto
    {
        public List<ErrorGroupDto> ErrorGroups { get; set; } = new();
        public List<ErrorCategoryDto> ErrorCategories { get; set; } = new();
        public List<ProductCategoryDto> ProductCategories { get; set; } = new();
        public List<string> ErrorGroupNamesExcept { get; set; } = new();
        public List<string> ProductCategoryNamesExcept { get; set; } = new();
        public List<string> ErrorCategoryNamesExcept { get; set; } = new();
        public List<ErrorExcelDto> Excel { get; set; } = new();
    }
}
