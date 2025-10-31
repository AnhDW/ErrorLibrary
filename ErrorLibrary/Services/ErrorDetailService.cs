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
    public class ErrorDetailService : IErrorDetailService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorDetailService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ErrorDetail errorDetail)
        {
            _context.ErrorDetails.Add(errorDetail);
        }

        public void Delete(ErrorDetail errorDetail)
        {
            _context.ErrorDetails.Remove(errorDetail);
        }

        public Task<PagedList<ErrorDetailDto>> GetAll(ErrorDetailParams errorDetailParams)
        {
            var query = _context.ErrorDetails.AsQueryable();

            return PagedList<ErrorDetailDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ErrorDetailDto>(_mapper.ConfigurationProvider),
                errorDetailParams.PageNumber,
                errorDetailParams.PageSize
                );
        }

        public async Task<List<ErrorDetail>> GetAll()
        {
            return await _context.ErrorDetails
                .Include(x=>x.Line).ThenInclude(x=>x.Enterprise).ThenInclude(x=>x.Factory).ThenInclude(x=>x.Unit)
                .Include(x=>x.Product)
                .Include(x=>x.Error)
                .Include(x=>x.User).ToListAsync();
        }

        public async Task<ErrorDetail> GetById(int lineId, int productId, int errorId, string userId)
        {
            return (await _context.ErrorDetails.FindAsync(lineId, productId, errorId, userId))!;
        }

        public void Update(ErrorDetail errorDetail)
        {
            _context.ErrorDetails.Update(errorDetail);
        }
    }
}
