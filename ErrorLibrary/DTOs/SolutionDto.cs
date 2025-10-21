namespace ErrorLibrary.DTOs
{
    public class SolutionDto
    {
        public int Id { get; set; }
        public int ErrorId { get; set; }
        public string Cause { get; set; }
        public string Handle { get; set; }
        public string? BeforeUrl { get; set; }
        public string? AfterUrl { get; set; }
    }
}
