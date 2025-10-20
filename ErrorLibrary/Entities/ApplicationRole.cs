using Microsoft.AspNetCore.Identity;

namespace ErrorLibrary.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}
