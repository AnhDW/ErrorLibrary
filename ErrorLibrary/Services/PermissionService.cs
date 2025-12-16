using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermissionService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Permission unit)
        {
            _context.Permissions.Add(unit);
        }

        public async Task<List<PermissionTreeDto>> GetPermissionTreeAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            var entities = permissions
                .Select(p => p.EntityName)
                .Distinct();
            var nodes = new List<PermissionTreeDto>();
            foreach (var entity in entities)
            {
                nodes.Add(new PermissionTreeDto
                {
                    Id = entity,
                    Parent = "#",
                    Text = entity,
                });
            }
            foreach (var permission in permissions)
            {
                var node = new PermissionTreeDto
                {
                    Id = permission.Id.ToString(),
                    Parent = permission.EntityName,
                    Text = permission.Action
                };
                nodes.Add(node);
            }
            return nodes;
        }

        public void Delete(Permission unit)
        {
            _context.Permissions.Remove(unit);
        }

        public async Task<List<Permission>> GetAll()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetById(int id)
        {
            return (await _context.Permissions.FindAsync(id))!;
        }

        public void Update(Permission unit)
        {
            _context.Permissions.Update(unit);
        }
    }
}
