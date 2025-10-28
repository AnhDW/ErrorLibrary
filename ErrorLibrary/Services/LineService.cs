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
    public class LineService : ILineService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LineService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Line line)
        {
            _context.Lines.Add(line);
        }

        public void Delete(Line line)
        {
            _context.Lines.Remove(line);
        }

        public async Task<PagedList<LineDto>> GetAll(LineParam lineParam)
        {
            var query = _context.Lines.AsQueryable();

            return await PagedList<LineDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<LineDto>(_mapper.ConfigurationProvider),
                lineParam.PageNumber,
                lineParam.PageSize);
        }

        public async Task<List<Line>> GetAll()
        {
            return await _context.Lines.ToListAsync();
        }

        public async Task<Line> GetById(int id)
        {
            return (await _context.Lines.FindAsync(id))!;
        }

        public void Update(Line line)
        {
            _context.Lines.Update(line);
        }
    }
}
