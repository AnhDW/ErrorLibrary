namespace ErrorLibrary.Entities
{
    public class Style
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ReportFinalFactoryDetail> ReportFinalFactoryDetails { get; set; } = new List<ReportFinalFactoryDetail>();
    }
}
