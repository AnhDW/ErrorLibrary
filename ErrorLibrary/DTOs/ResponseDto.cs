using ErrorLibrary.Helper;

namespace ErrorLibrary.DTOs
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public PaginationHeader? PaginationHeader { get; set; }
    }
}
