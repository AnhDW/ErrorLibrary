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
    public class TimeFrameService : ITimeFrameService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TimeFrameService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(TimeFrame timeFrame)
        {
            _context.TimeFrames.Add(timeFrame);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.TimeFrames.AnyAsync(x => x.Name == name);
        }

        public string CreateTitle(TimeOnly startTime, TimeOnly endTime)
        {
            return $"{startTime:HH\\:mm} - {endTime:HH\\:mm}";
        }

        public void Delete(TimeFrame timeFrame)
        {
            _context.TimeFrames.Remove(timeFrame);
        }

        public Task<PagedList<TimeFrameDto>> GetAll(TimeFrameParams timeFrameParams)
        {
            var query = _context.TimeFrames.AsQueryable();

            return PagedList<TimeFrameDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<TimeFrameDto>(_mapper.ConfigurationProvider),
                timeFrameParams.PageNumber,
                timeFrameParams.PageSize);
        }

        public async Task<List<TimeFrame>> GetAll()
        {
            return await _context.TimeFrames.ToListAsync();
        }

        public async Task<TimeFrame> GetById(int id)
        {
            return (await _context.TimeFrames
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public void Update(TimeFrame timeFrame)
        {
            _context.TimeFrames.Update(timeFrame);
        }
    }
}
