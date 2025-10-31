namespace ErrorLibrary.DTOs
{
    public class UpdateOrganizationsByUserDto
    {
        public string UserId { get; set; }
        public List<OrganizationDto> Organizations { get; set; } = new List<OrganizationDto>();
    }
}
