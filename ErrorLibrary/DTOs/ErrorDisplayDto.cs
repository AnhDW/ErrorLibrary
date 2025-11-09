namespace ErrorLibrary.DTOs
{
    public class ErrorDisplayDto
    {
        public int Id { get; set; }
        public int ErrorGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ErrorCategory { get; set; }
        public ErrorGroupDto? ErrorGroup { get; set; }
    }
}
