namespace ErrorLibrary.DTOs
{
    public class EndLineDetailDto
    {
        public int Id { get; set; }
        public int EndLineId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
