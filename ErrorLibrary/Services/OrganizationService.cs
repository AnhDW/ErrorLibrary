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
            using var unitsContext = _dbContextFactory.CreateDbContext();
            using var factoriesContext = _dbContextFactory.CreateDbContext();
            using var enterprisesContext = _dbContextFactory.CreateDbContext();
            using var linesContext = _dbContextFactory.CreateDbContext();
            var unitsTask = unitsContext.Units.Select(x => new { x.Id, x.Name }).ToListAsync();
            var factoriesTask = factoriesContext.Factories.Select(x => new { x.Id, x.Name, x.UnitId }).ToListAsync();
            var enterprisesTask = enterprisesContext.Enterprises.Select(x => new { x.Id, x.Name, x.FactoryId }).ToListAsync();
            var linesTask = linesContext.Lines.Select(x => new { x.Id, x.Name, x.EnterpriseId }).ToListAsync();

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
                id = $"unit_{u.Id}",
                text = u.Name,
                icon = "fa-solid fa-building",
                children = factoriesByUnit.TryGetValue(u.Id, out var factories) ?
                factories.Select(f => new
                {
                    id = $"factory_{f.Id}",
                    text = f.Name,
                    icon = "fa-solid fa-industry",
                    children = enterprisesByFactory.TryGetValue(f.Id, out var enterprises) ?
                    enterprises.Select(e => new
                    {
                        id = $"enterprise_{e.Id}",
                        text = e.Name,
                        icon = "fa-solid fa-store",
                        children = linesByEnterprise.TryGetValue(e.Id, out var lines) ?
                        lines.Select(l => new
                        {
                            id = $"line_{l.Id}",
                            text = l.Name,
                            icon = "fa-solid fa-pallet",
                        }) : null
                    }) : null
                }) : null
            });

            return tree;
        }
    }
}
