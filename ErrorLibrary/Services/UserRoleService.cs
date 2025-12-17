using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRoleService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(IdentityUserRole<string> identityUserRole)
        {
            _context.UserRoles.Add(identityUserRole);
        }

        public void Delete(IdentityUserRole<string> identityUserRole)
        {
            _context.UserRoles.Remove(identityUserRole);
        }

        public async Task<List<IdentityUserRole<string>>> GetAll()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<IdentityUserRole<string>> GetById(string userId, string roleId)
        {
            return (await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId))!;
        }

        public async Task<List<string>> GetRoleIdsByUserId(string userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();
        }

        public async Task<List<string>> GetUserIdsByRoleId(string roleId)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.UserId)
                .ToListAsync();
        }
    }
}
