namespace ErrorLibrary.Entities
{
    public class Factory
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Unit Unit { get; set; }
        public List<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
    }
}
