using Microsoft.AspNetCore.Authorization;

namespace ErrorLibrary.Authorization.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string entity, string action)
        {
            Policy = $"PERMISSION_{entity}.{action}";
        }
    }

}
