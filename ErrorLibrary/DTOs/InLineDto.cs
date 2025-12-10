namespace ErrorLibrary.DTOs
{
    public class InLineDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsFinalized { get; set; } = false;
    }
}
