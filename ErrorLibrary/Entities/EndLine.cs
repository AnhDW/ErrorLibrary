namespace ErrorLibrary.Entities
{
    public class EndLine
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; } = 0;
        public int CheckQuantity { get; set; } = 0;
        public int AcceptedQuantity { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsFinalized { get; set; } = false;
        public DateOnly Date { get; set; }
        public Line Line { get; set; }
        public Product Product { get; set; }
        public List<EndLineDetail> EndLineDetails { get; set; } = new();
    }
}
