using ErrorLibrary.Entities;
using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.Data
{
    public static class PermissionSeeder
    {
        public static IEnumerable<Permission> Generate()
        {
            int id = 1;

            foreach (var entity in Enum.GetValues<PermissionEntity>())
            {
                foreach (var action in Enum.GetValues<PermissionAction>())
                {
                    yield return new Permission
                    {
                        Id = id++,
                        EntityName = entity.ToString(),
                        Action = action.ToString(),
                        Code = $"{entity}.{action}",
                        Description = $"{action} {entity}"
                    };
                }
            }
        }
    }

}
