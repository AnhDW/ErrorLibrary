namespace ErrorLibrary.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
