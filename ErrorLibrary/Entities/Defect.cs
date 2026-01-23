namespace ErrorLibrary.Entities
{
    public class Defect
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<ReportFinalFactoryDetailDefect> ReportFinalFactoryDetailDefects { get; set; } = new List<ReportFinalFactoryDetailDefect>();
    }
}
