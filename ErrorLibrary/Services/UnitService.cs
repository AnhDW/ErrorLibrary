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
    public class UnitService : IUnitService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UnitService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Unit unit)
        {
            _context.Units.Add(unit);
        }

        public void Delete(Unit unit)
        {
            _context.Units.Remove(unit);
        }

        public async Task<PagedList<UnitDto>> GetAll(UnitParam unitParam)
        {
            var query = _context.Units.AsQueryable();

            return await PagedList<UnitDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<UnitDto>(_mapper.ConfigurationProvider),
                unitParam.PageNumber,
                unitParam.PageSize);
        }

        public async Task<List<Unit>> GetAll()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit> GetById(int id)
        {
            return (await _context.Units.FindAsync(id))!;
        }

        public void Update(Unit unit)
        {
            _context.Units.Update(unit);
        }
    }
}
