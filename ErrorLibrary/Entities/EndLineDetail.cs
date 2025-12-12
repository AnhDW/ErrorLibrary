namespace ErrorLibrary.Entities
{
    public class EndLineDetail
    {
        public int Id { get; set; }
        public int EndLineId { get; set; }
        public int ErrorId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public EndLine EndLine { get; set; }
        public Error Error { get; set; }
        public ApplicationUser User { get; set; }
    }
}
