namespace ErrorLibrary.Entities
{
    public class Line
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Enterprise Enterprise { get; set; }
        public List<InLine> InLines { get; set; } = new();
        public List<EndLine> EndLines { get; set; } = new();
        public List<ErrorDetail> ErrorDetails { get; set; } = new();

    }
}
