namespace ErrorLibrary.Entities
{
    public class UserOrganization
    {
        public string UserId {  get; set; }
        public string OrganizationType { get; set; }
        public int OrganizationId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
