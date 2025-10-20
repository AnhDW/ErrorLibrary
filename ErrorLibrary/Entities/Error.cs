namespace ErrorLibrary.Entities
{
    public class Error
    {
        public int Id { get; set; }
        public int ErrorCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ErrorCategory ErrorCategory { get; set; }
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}
