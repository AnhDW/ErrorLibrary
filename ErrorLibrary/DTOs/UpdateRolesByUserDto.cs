namespace ErrorLibrary.DTOs
{
    public class UpdateRolesByUserDto
    {
        public string UserId { get; set; }
        public List<string> RoleIds { get; set; } = new List<string>();
    }
}
