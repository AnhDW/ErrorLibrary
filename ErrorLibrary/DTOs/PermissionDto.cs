namespace ErrorLibrary.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
