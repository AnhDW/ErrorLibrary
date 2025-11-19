namespace ErrorLibrary.DTOs
{
    public class ErrorDetailAttachmentDto
    {
        public int Id { get; set; }
        public int ErrorDetailId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
