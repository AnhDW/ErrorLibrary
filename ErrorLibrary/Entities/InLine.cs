namespace ErrorLibrary.Entities
{
    public class InLine
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateOnly DateCreate { get; set; }
        public int Quantity { get; set; } = 0;

        public Line Line { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
        public List<InLineDetail> InLineDetails { get; set; } = new();
    }
}
