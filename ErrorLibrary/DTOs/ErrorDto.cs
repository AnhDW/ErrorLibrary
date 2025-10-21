namespace ErrorLibrary.DTOs
{
    public class ErrorDto
    {
        public int Id { get; set; }
        public int ErrorCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
