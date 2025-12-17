namespace ErrorLibrary.DTOs
{
    public class UpdatePermissionsByRoleDto
    {
        public string RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}
