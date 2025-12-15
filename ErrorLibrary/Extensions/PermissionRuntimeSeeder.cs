using ErrorLibrary.Data;
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

            if (permissions.Any())
            {
                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }
        }
    }

}
