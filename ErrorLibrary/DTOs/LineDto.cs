using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class LineDto
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EnterpriseDto? Enterprise { get; set; }
    }
}
