using Microsoft.AspNetCore.Identity;

namespace ErrorLibrary.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Code {  get; set; }
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }

        public List<ErrorDetail> ErrorDetails { get; set; } = new List<ErrorDetail>();
    }
}
