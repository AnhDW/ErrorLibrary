namespace ErrorLibrary.Entities
{
    public class ReportFinalFactoryDetailDefect
    {
        public int ReportFinalFactoryDetailId { get; set; }
        public int DefectId { get; set; }
        public int Quantity { get; set; } = 0;

        public ReportFinalFactoryDetail ReportFinalFactoryDetail { get; set; }
        public Defect Defect { get; set; }
    }
}
