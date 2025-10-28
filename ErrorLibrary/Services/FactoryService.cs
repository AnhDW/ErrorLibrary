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
    public class FactoryService : IFactoryService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FactoryService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Factory factory)
        {
            _context.Factories.Add(factory);
        }

        public void Delete(Factory factory)
        {
            _context.Factories.Remove(factory);
        }

        public async Task<PagedList<FactoryDto>> GetAll(FactoryParam factoryParam)
        {
            var query = _context.Factories.AsQueryable();

            return await PagedList<FactoryDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<FactoryDto>(_mapper.ConfigurationProvider),
                factoryParam.PageNumber,
                factoryParam.PageSize);
        }

        public async Task<List<Factory>> GetAll()
        {
            return await _context.Factories.ToListAsync();
        }

        public async Task<Factory> GetById(int id)
        {
            return (await _context.Factories.FindAsync(id))!;
        }

        public void Update(Factory factory)
        {
            _context.Factories.Update(factory);
        }
    }
}
