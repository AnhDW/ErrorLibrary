
namespace ErrorLibrary.DTOs
{
    public class EndLineDetailDisplayDto
    {
        public int Id { get; set; }
        public int EndLineId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public EndLineDto EndLine { get; set; }
        public ErrorDisplayDto Error { get; set; }
        public UserDto User { get; set; }
    }
}
