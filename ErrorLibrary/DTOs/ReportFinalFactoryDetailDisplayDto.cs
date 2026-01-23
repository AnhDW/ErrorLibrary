using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class ReportFinalFactoryDetailDisplayDto
    {
        public int Id { get; set; }
        public int ReportFinalFactoryId { get; set; }
        public int CustomerId { get; set; }
        public int StyleId { get; set; }
        public string PO { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public CustomerDto Customer { get; set; }
        public StyleDto Style { get; set; }

        public List<InspectionDisplayDto> Inspections { get; set; } = new();
        public List<ReportFinalFactoryDetailDefectDto> ReportFinalFactoryDetailDefects { get; set; } = new();
    }
}
