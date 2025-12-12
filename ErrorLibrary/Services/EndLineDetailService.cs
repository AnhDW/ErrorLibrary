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
    public class EndLineDetailService : IEndLineDetailService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EndLineDetailService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(EndLineDetail endLineDetail)
        {
            _context.EndLineDetails.Add(endLineDetail);
        }

        public HashSet<string> BuildExistingEndLineDetailKeySet(List<EndLineDetail> endLineDetails)
        {
            return endLineDetails.Select(x => $"{x.EndLineId}|{x.ErrorId}|{x.UserId}").ToHashSet();
        }

        public async Task<bool> CheckExists(int endLineId, int errorId, string userId)
        {
            return await _context.EndLineDetails.AnyAsync(x =>
                x.EndLineId == endLineId &&
                x.ErrorId == errorId &&
                x.UserId == userId);
        }

        public bool CheckExists(HashSet<string> existingKeys, int endLineId, int errorId, string userId)
        {
            var key = $"{endLineId}|{errorId}|{userId}";
            return existingKeys.Contains(key);
        }

        public void Delete(EndLineDetail endLineDetail)
        {
            _context.EndLineDetails.Remove(endLineDetail);
        }

        public Task<PagedList<EndLineDetailDto>> GetAll(EndLineDetailParams endLineDetailParam)
        {
            var query = _context.EndLineDetails.AsQueryable();

            return PagedList<EndLineDetailDto>.CreateAsync(
                query.ProjectTo<EndLineDetailDto>(_mapper.ConfigurationProvider),
                endLineDetailParam.PageNumber,
                endLineDetailParam.PageSize);
        }

        public async Task<List<EndLineDetail>> GetAll()
        {
            return await _context.EndLineDetails.ToListAsync();
        }

        public async Task<EndLineDetail> GetById(int id)
        {
            return (await _context.EndLineDetails.FindAsync(id))!;
        }

        public void Update(EndLineDetail endLineDetail)
        {
            _context.EndLineDetails.Update(endLineDetail);
        }
    }
}
