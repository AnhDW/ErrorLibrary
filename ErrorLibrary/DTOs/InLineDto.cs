namespace ErrorLibrary.DTOs
{
    public class InLineDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateOnly DateCreate { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
