using ErrorLibrary.Entities;
using System.Security.Claims;

namespace ErrorLibrary.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
        ClaimsPrincipal ValidateToken(string token);
    }
}
