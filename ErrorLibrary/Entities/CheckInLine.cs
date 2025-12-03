namespace ErrorLibrary.Entities
{
    public class CheckInLine
    {
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; } = 0;
        public DateOnly DateCreate { get; set; }
    }
}
