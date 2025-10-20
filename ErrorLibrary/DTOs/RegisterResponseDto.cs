namespace ErrorLibrary.DTOs
{
    public class RegisterResponseDto
    {
        public object User { get; set; } = null!;
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
