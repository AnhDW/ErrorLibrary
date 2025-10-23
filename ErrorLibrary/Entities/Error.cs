namespace ErrorLibrary.Entities
{
    public class Error
    {
        public int Id { get; set; }
        public int ErrorGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ErrorCategory { get; set; }
        public ErrorGroup ErrorGroup { get; set; }
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}
