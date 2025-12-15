using ErrorLibrary.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace ErrorLibrary.Authorization.Policies
{
    public class PermissionPolicyProvider
    : IAuthorizationPolicyProvider
    {
        private const string PREFIX = "PERMISSION_";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(PREFIX))
            {
                var permission = policyName.Substring(PREFIX.Length);

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(permission))
                    .Build();

                return Task.FromResult(policy);
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => Task.FromResult(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
            => Task.FromResult<AuthorizationPolicy>(null);
    }

}
