namespace ErrorLibrary.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }
        public int FactoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Factory Factory { get; set; }
        public List<Line> Lines { get; set; } = new List<Line>();
    }
}
