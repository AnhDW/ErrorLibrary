using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class InLineDisplayDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsFinalized { get; set; } = false;

        public LineDto Line { get; set; }
        public ProductDto Product { get; set; }
        public UserDto User { get; set; }
        public List<InLineDetailDto> InLineDetails { get; set; } = new();
    }
}
