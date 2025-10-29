using AutoMapper;
using AutoMapper.QueryableExtensions;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnterpriseService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Enterprise enterprise)
        {
            _context.Enterprises.Add(enterprise);
        }

        public async Task<bool> CheckNameExists(string name, int factoryId)
        {
            return await _context.Enterprises.AnyAsync(x => x.Name == name && x.FactoryId == factoryId);
        }

        public void Delete(Enterprise enterprise)
        {
            _context.Enterprises.Remove(enterprise);
        }

        public async Task<PagedList<EnterpriseDto>> GetAll(EnterpriseParams enterpriseParam)
        {
            var query = _context.Enterprises.AsQueryable();
            if(enterpriseParam.FactoryId != null)
            {
                query = query.Where(x=>x.FactoryId == enterpriseParam.FactoryId);
            }
            return await PagedList<EnterpriseDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<EnterpriseDto>(_mapper.ConfigurationProvider),
                enterpriseParam.PageNumber,
                enterpriseParam.PageSize);
        }

        public async Task<List<Enterprise>> GetAll()
        {
            return await _context.Enterprises.Include(x=>x.Factory).ThenInclude(x=>x.Unit).ToListAsync();
        }

        public async Task<List<Enterprise>> GetAllByFactoryId(int factoryId)
        {
            return await _context.Enterprises.Where(x=>x.FactoryId == factoryId).ToListAsync();
        }

        public async Task<Enterprise> GetById(int id)
        {
            return ( await _context.Enterprises.FindAsync(id))!;
        }

        public void Update(Enterprise enterprise)
        {
            _context.Enterprises.Update(enterprise);    
        }
    }
}
