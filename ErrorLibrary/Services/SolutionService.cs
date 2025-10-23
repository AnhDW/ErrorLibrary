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
    public class SolutionService : ISolutionService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SolutionService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Solution solution)
        {
            _context.Solutions.Add(solution);
        }

        public void Delete(Solution solution)
        {
            _context.Solutions.Remove(solution);
        }

        public async Task<PagedList<SolutionDto>> GetAll(SolutionParams solutionParams)
        {
            var query = _context.Products.AsQueryable();
            return await PagedList<SolutionDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<SolutionDto>(_mapper.ConfigurationProvider),
                solutionParams.PageNumber,
                solutionParams.PageSize);
        }

        public async Task<List<Solution>> GetAll()
        {
            return await _context.Solutions.Include(x => x.Error).ToListAsync();
        }

        public async Task<Solution> GetById(int id)
        {
            return (await _context.Solutions.FindAsync(id))!;
        }

        public void Update(Solution solution)
        {
            _context.Solutions.Update(solution);
        }
    }
}
