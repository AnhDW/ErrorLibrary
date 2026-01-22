namespace ErrorLibrary.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
