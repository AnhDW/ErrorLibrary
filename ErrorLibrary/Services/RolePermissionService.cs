
using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RolePermissionService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(RolePermission rolePermission)
        {
            _context.RolePermissions.Add(rolePermission);
        }

        public void Delete(RolePermission rolePermission)
        {
            _context.RolePermissions.Remove(rolePermission);
        }

        public async Task<List<RolePermission>> GetAllRoles()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task<RolePermission> GetById(string role, int permissionId)
        {
            return (await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == role && rp.PermissionId == permissionId))!;
        }

        public async Task<List<int>> GetPermissionIdsByRoleId(string roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();
        }

        public async Task<List<string>> GetRoleIdsByPermissionId(int permissionId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(rp => rp.RoleId)
                .ToListAsync();
        }
    }
}
