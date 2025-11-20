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

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
