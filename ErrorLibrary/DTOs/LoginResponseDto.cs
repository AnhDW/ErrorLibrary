namespace ErrorLibrary.DTOs
{
    public class LoginResponseDto
    {
        public object User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
