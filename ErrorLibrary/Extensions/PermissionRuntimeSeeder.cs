using ErrorLibrary.Data;
using ErrorLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Extensions
{
    public static class PermissionRuntimeSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var existingCodes = await context.Permissions
                .Select(p => p.Code)
                .ToListAsync();

            var permissions = PermissionSeeder.Generate()
                .Where(p => !existingCodes.Contains(p.Code))
                .ToList();
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Admin");
            var rolePermissions = new List<RolePermission>();
            foreach ( var permission in permissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    RoleId = adminRole.Id,
                    Permission = permission
                });
            }

            if (permissions.Any())
            {
                context.RolePermissions.AddRange(rolePermissions);
                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }
        }
    }

}
