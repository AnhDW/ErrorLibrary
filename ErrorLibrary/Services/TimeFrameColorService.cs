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

        public void AddRange(List<TimeFrameColor> timeFrameColors)
        {
            _context.TimeFrameColors.AddRange(timeFrameColors);
        }

        public async Task<bool> CheckExists(int timeFrameId, string hexCode)
        {
            return await _context.TimeFrameColors.AnyAsync(x => x.TimeFrameId == timeFrameId && x.HexCode == hexCode);
        }

        public void Delete(TimeFrameColor timeFrameColor)
        {
            _context.TimeFrameColors.Remove(timeFrameColor);
        }

        public void DeleteRange(List<TimeFrameColor> timeFrameColors)
        {
            _context.TimeFrameColors.RemoveRange(timeFrameColors);
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

        public async Task<List<TimeFrameColor>> GetByIds(List<int> ids)
        {
            return (await _context.TimeFrameColors
                .Where(x => ids.Contains(x.Id)).ToListAsync())!;
        }

        public async Task<List<TimeFrameColor>> GetByTimeFrame(int timeFrameId)
        {
            return await _context.TimeFrameColors
                .Where(x => x.TimeFrameId == timeFrameId)
                .ToListAsync();
        }

        public void Update(TimeFrameColor timeFrameColor)
        {
            _context.TimeFrameColors.Update(timeFrameColor);
        }

        public void UpdateRange(List<TimeFrameColor> timeFrameColors)
        {
            _context.TimeFrameColors.UpdateRange(timeFrameColors);
        }
    }
}
