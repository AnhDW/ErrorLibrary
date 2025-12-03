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
    public class TimeFrameColorService : ITimeFrameColorService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TimeFrameColorService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(TimeFrameColor timeFrameColor)
        {
            _context.TimeFrameColors.Add(timeFrameColor);
        }

        public void Delete(TimeFrameColor timeFrameColor)
        {
            _context.TimeFrameColors.Remove(timeFrameColor);
        }

        public Task<PagedList<TimeFrameColorDto>> GetAll(TimeFrameColorParams timeFrameColorParams)
        {
            var query = _context.TimeFrameColors.AsQueryable();

            return PagedList<TimeFrameColorDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<TimeFrameColorDto>(_mapper.ConfigurationProvider),
                timeFrameColorParams.PageNumber,
                timeFrameColorParams.PageSize);
        }

        public async Task<List<TimeFrameColor>> GetAll()
        {
            return await _context.TimeFrameColors.ToListAsync();
        }

        public async Task<TimeFrameColor> GetById(int id)
        {
            return (await _context.TimeFrameColors
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public void Update(TimeFrameColor timeFrameColor)
        {
            _context.TimeFrameColors.Update(timeFrameColor);
        }
    }
}
