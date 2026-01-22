namespace ErrorLibrary.DTOs
{
    public class ReportFinalFactoryDetailDto
    {
        public int Id { get; set; }
        public int ReportFinalFactoryId { get; set; }
        public int CustomerId { get; set; }
        public int StyleId { get; set; }
        public string PO { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
