namespace ErrorLibrary.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Factory> Factories { get; set; } = new List<Factory>();
    }
}
