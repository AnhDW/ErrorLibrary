using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.Entities
{
    public class Inspection
    {
        public int Id { get; set; }
        public int ReportFinalFactoryDetailId { get; set; }
        public InspectionType InspectionType { get; set; }
        public int Major { get; set; } = 0;
        public int Minor { get; set; } = 0;

        public ReportFinalFactoryDetail ReportFinalFactoryDetail { get; set; }
        public List<InspectionRound> InspectionRounds { get; set; } = new List<InspectionRound>();
    }
}
