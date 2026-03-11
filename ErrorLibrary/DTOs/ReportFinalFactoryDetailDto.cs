namespace ErrorLibrary.DTOs
{
    public class ReportFinalFactoryDetailDto
    {
        public int Id { get; set; }
        public int ReportFinalFactoryId { get; set; }
        public string CustomerCode { get; set; }
        public string StyleCode { get; set; }
        public string PO { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
