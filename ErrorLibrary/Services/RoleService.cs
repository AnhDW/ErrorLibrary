using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class RoleService : IRoleService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ApplicationRole role)
        {
            _context.Roles.Add(role);
        }

        public Task<bool> CheckNameExists(string name)
        {
            return _context.Roles.AnyAsync(r => r.Name == name);
        }

        public void Delete(ApplicationRole role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<List<ApplicationRole>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> GetById(string id)
        {
            return (await _context.Roles.FindAsync(id))!;
        }

        public async Task<List<ApplicationRole>> GetRoleByRoleIds(List<string> roleIds)
        {
            return await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .ToListAsync();
        }

        public void Update(ApplicationRole role)
        {
            _context.Roles.Update(role);
        }
    }
}
