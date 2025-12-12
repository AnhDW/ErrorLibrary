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
    public class EndLineService : IEndLineService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EndLineService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(EndLine endLine)
        {
            _context.EndLines.Add(endLine);
        }

        public HashSet<string> BuildExistingEndLineKeySet(List<EndLine> endLines)
        {
            return endLines.Select(x => $"{x.LineId}|{x.ProductId}|{x.Date}").ToHashSet();
        }

        public async Task<bool> CheckExists(int lineId, int productId, DateOnly date)
        {
            return await _context.EndLines.AnyAsync(x =>
                x.LineId == lineId &&
                x.ProductId == productId &&
                x.Date == date);
        }

        public bool CheckExists(HashSet<string> existingKeys, int lineId, int productId, DateOnly date)
        {
            var key = $"{lineId}|{productId}|{date}";
            return existingKeys.Contains(key);
        }

        public void Delete(EndLine endLine)
        {
            _context.EndLines.Remove(endLine);
        }

        public Task<PagedList<EndLineDto>> GetAll(EndLineParams endLineParam)
        {
            var query = _context.EndLines.AsQueryable();

            return PagedList<EndLineDto>.CreateAsync(
                query.ProjectTo<EndLineDto>(_mapper.ConfigurationProvider),
                endLineParam.PageNumber,
                endLineParam.PageSize);
        }

        public async Task<List<EndLine>> GetAll()
        {
            return await _context.EndLines.ToListAsync();
        }

        public async Task<EndLine> GetById(int id)
        {
            return (await _context.EndLines.FindAsync(id))!;
        }

        public void Update(EndLine endLine)
        {
            _context.EndLines.Update(endLine);
        }
    }
}
