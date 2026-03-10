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
        public int Quantity { get; set; } = 0;
        //pre-final
        public int PreFinalMajor { get; set; } = 0;
        public int PreFinalMinor { get; set; } = 0;
        public DateTime? PreFinalDate1 { get; set; }
        public Result? PreFinalResult1 { get; set; }
        public DateTime? PreFinalPreFinalDate2 { get; set; }
        public Result? PreFinalResult2 { get; set; }
        public DateTime? PreFinalPreFinalDate3 { get; set; }
        public Result? PreFinalResult3 { get; set; }
        //final
        public int FinalMajor { get; set; } = 0;
        public int FinalMinor { get; set; } = 0;
        public DateTime? FinalDate1 { get; set; }
        public Result? FinalResult1 { get; set; }
        public DateTime? FinalDate2 { get; set; }
        public Result? FinalResult2 { get; set; }
        public DateTime? FinalDate3 { get; set; }
        public Result? FinalResult3 { get; set; }

        public Customer Customer { get; set; }
        public Style Style { get; set; }
        public ReportFinalFactory ReportFinalFactory { get; set; }

        //public List<Inspection> Inspections { get; set; } = new List<Inspection>();
        public List<ReportFinalFactoryDetailDefect> ReportFinalFactoryDetailDefects { get; set; } = new List<ReportFinalFactoryDetailDefect>();
    }
}
