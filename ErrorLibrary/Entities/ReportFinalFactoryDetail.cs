using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.Entities
{
    public class ReportFinalFactoryDetail
    {
        public int Id { get; set; }
        public int ReportFinalFactoryId { get; set; }
        public int CustomerId { get; set; }
        public int StyleId { get; set; }
        public string PO { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public Customer Customer { get; set; }
        public Style Style { get; set; }
        public ReportFinalFactory ReportFinalFactory { get; set; }

        public List<Inspection> Inspections { get; set; } = new List<Inspection>();
        public List<InspectionDefect> InspectionDefects { get; set; } = new List<InspectionDefect>();
    }
}
