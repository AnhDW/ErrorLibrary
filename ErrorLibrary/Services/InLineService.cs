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
    public class InLineService : IInLineService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InLineService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(InLine inLine)
        {
            _context.InLines.Add(inLine);
        }

        public HashSet<string> BuildExistingInLineKeySet(List<InLine> inLines)
        {
            return inLines.Select(x => $"{x.LineId}|{x.ProductId}|{x.UserId}|{x.Date}").ToHashSet();
        }

        public Task<bool> CheckExists(int inLineId, int productId, string userId, DateOnly date)
        {
            return _context.InLines.AnyAsync(x =>
                x.LineId == inLineId &&
                x.ProductId == productId &&
                x.UserId == userId &&
                x.Date == date);
        }

        public bool CheckNameExistsFast(HashSet<string> existingKeys, int lineId, int productId, string userId, DateOnly date)
        {
            var key = $"{lineId}|{productId}|{userId}|{date}";
            return existingKeys.Contains(key);
        }

        public void Delete(InLine inLine)
        {
            _context.InLines.Remove(inLine);
        }

        public Task<PagedList<InLineDto>> GetAll(InLineParams inLineParams)
        {
            var query = _context.InLines.AsQueryable();

            return PagedList<InLineDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<InLineDto>(_mapper.ConfigurationProvider),
                inLineParams.PageNumber,
                inLineParams.PageSize);
        }

        public async Task<List<InLine>> GetAll()
        {
            return await _context.InLines.ToListAsync();
        }

        public async Task<InLine> GetById(int id)
        {
            return (await _context.InLines
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public void Update(InLine inLine)
        {
            _context.InLines.Update(inLine);
        }
    }
}
