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

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int I { get; set; }
        public int J { get; set; }
        public int K { get; set; }
        public int L { get; set; }
        public int M { get; set; }
        public int N { get; set; }
        public int O { get; set; }
        public int P { get; set; }
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }
        public int T { get; set; }
        public int U { get; set; }
        public int V { get; set; }
        public int W { get; set; }

        public Customer Customer { get; set; }
        public Style Style { get; set; }
        public ReportFinalFactory ReportFinalFactory { get; set; }

        public List<Inspection> Inspections { get; set; } = new List<Inspection>();
    }
}
