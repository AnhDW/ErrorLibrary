using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class ErrorDetailDto
    {
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }

        public LineDto? Line { get; set; }
        public ProductDto? Product { get; set; }
        public ErrorDto? Error { get; set; }
        public UserDto? User { get; set; }
    }
}
