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
    public class InLineDetailService : IInLineDetailService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InLineDetailService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(InLineDetail inLineDetail)
        {
            _context.InLineDetails.Add(inLineDetail);
        }

        public async Task<bool> CheckExists(int inLineId, int timeFrameId, int errorId)
        {
            return await _context.InLineDetails.AnyAsync(x =>
                x.InLineId == inLineId &&
                x.TimeFrameId == timeFrameId &&
                x.ErrorId == errorId);
        }

        public void Delete(InLineDetail inLineDetail)
        {
            _context.InLineDetails.Remove(inLineDetail);
        }

        public Task<PagedList<InLineDetailDisplayDto>> GetAll(InLineDetailParams inLineDetailParams)
        {
            var query = _context.InLineDetails.AsQueryable();

            if (inLineDetailParams.TimeFrameIds.Count > 0)
            {
                query = query.Where(x => inLineDetailParams.TimeFrameIds.Contains(x.TimeFrameId));
            }

            return PagedList<InLineDetailDisplayDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<InLineDetailDisplayDto>(_mapper.ConfigurationProvider),
                inLineDetailParams.PageNumber,
                inLineDetailParams.PageSize);
        }

        public async Task<List<InLineDetail>> GetAll()
        {
            return await _context.InLineDetails.ToListAsync();
        }

        public async Task<InLineDetail> GetById(int id)
        {
            return (await _context.InLineDetails
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public void Update(InLineDetail inLineDetail)
        {
            _context.InLineDetails.Update(inLineDetail);
        }
    }
}
