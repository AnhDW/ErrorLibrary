namespace ErrorLibrary.Entities
{
    public class ErrorDetail
    {
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }

        public Line Line { get; set; }
        public Product Product { get; set; }
        public Error Error { get; set;}
        public ApplicationUser User { get; set; }
    }
}
