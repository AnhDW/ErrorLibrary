namespace ErrorLibrary.DTOs
{
    public class ErrorDetailAttachmentDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public IFormFile File { get; set; }
    }
}
