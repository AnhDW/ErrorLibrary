using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetOrganizationTree()
        {
            var unitsTask = _context.Units.Select(x => new { x.Id, x.Name }).ToListAsync();
            var factoriesTask = _context.Factories.Select(x => new { x.Id, x.Name, x.UnitId }).ToListAsync();
            var enterprisesTask = _context.Enterprises.Select(x => new { x.Id, x.Name, x.FactoryId }).ToListAsync();
            var linesTask = _context.Lines.Select(x => new { x.Id, x.Name, x.EnterpriseId }).ToListAsync();

            await Task.WhenAll(unitsTask, factoriesTask, enterprisesTask, linesTask);

            var units = unitsTask.Result;
            var factories = factoriesTask.Result;
            var enterprises = enterprisesTask.Result;
            var lines = linesTask.Result;

            var factoriesByUnit = factories.GroupBy(x => x.UnitId).ToDictionary(g => g.Key, g => g.ToList());
            var enterprisesByFactory = enterprises.GroupBy(x => x.FactoryId).ToDictionary(g => g.Key, g => g.ToList());
            var linesByEnterprise = lines.GroupBy(x => x.EnterpriseId).ToDictionary(g => g.Key, g => g.ToList());

            var tree = units.Select(u => new
            {
                value = $"unit_{u.Id}",
                lable = u.Name,
                childrens = factoriesByUnit.TryGetValue(u.Id, out var factories) ?
                factories.Select(f => new
                {
                    value = $"factory_{f.Id}",
                    lable = f.Name,
                    childrens = enterprisesByFactory.TryGetValue(f.Id, out var enterprises) ?
                    enterprises.Select(e => new
                    {
                        value = $"enterprise_{e.Id}",
                        lable = e.Name,
                        children = linesByEnterprise.TryGetValue(e.Id, out var lines) ?
                        lines.Select(l => new
                        {
                            value = $"line_{l.Id}",
                            lable = l.Name,
                        }) : null
                    }) : null
                }) : null
            });

            return tree;
        }
    }
}
