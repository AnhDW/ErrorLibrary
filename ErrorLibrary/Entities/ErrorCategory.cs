namespace ErrorLibrary.Entities
{
    public class ErrorCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
