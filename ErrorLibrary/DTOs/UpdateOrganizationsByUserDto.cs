namespace ErrorLibrary.DTOs
{
    public class UpdateOrganizationsByUserDto
    {
        public string UserId { get; set; }
        public List<(string organizationType, int organizationId)> Organizations { get; set; } = new List<(string organizationType, int organizationIdstring)>();
    }
}
