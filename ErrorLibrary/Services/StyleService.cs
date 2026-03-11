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
    public class StyleService : IStyleService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StyleService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Style style)
        {
            _context.Styles.Add(style);
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.Styles.AnyAsync(x => x.Code == code);
        }

        public void Delete(Style style)
        {
            _context.Styles.Remove(style);
        }

        public async Task<PagedList<StyleDto>> GetAll(StyleParams styleParam)
        {
            var query = _context.Styles.AsQueryable();
            return await PagedList<StyleDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<StyleDto>(_mapper.ConfigurationProvider),
                styleParam.PageNumber,
                styleParam.PageSize);
        }

        public async Task<List<Style>> GetAll()
        {
            return await _context.Styles.ToListAsync();
        }

        public async Task<Style> GetByCode(string code)
        {
            return (await _context.Styles.FirstOrDefaultAsync(x => x.Code == code)) ?? new Style { Code = code, CreatedAt = DateTime.Now };
        }

        public async Task<Style> GetById(int id)
        {
            return (await _context.Styles.FindAsync(id))!;
        }

        public void Update(Style style)
        {
            _context.Styles.Update(style);
        }
    }
}
