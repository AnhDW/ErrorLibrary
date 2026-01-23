namespace ErrorLibrary.Entities
{
    public class ReportFinalFactory
    {
        public int Id { get; set; }
        public int FactoryId { get; set; }
        public string Name { get; set; }
        public DateOnly CreateDate { get; set; }

        public Factory Factory { get; set; }
        public List<ReportFinalFactoryDetail> ReportFinalFactoryDetails { get; set; } = new List<ReportFinalFactoryDetail>();
    }
}
