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
    public class ReportFinalFactoryService : IReportFinalFactoryService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportFinalFactoryService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ReportFinalFactory reportFinalFactory)
        {
            _context.ReportFinalFactories.Add(reportFinalFactory);
        }

        public async Task<bool> CheckExists(int factoryId, DateOnly createDate)
        {
            return await _context.ReportFinalFactories.AnyAsync(x => x.FactoryId == factoryId && x.CreateDate == createDate);
        }

        public void Delete(ReportFinalFactory reportFinalFactory)
        {
            _context.ReportFinalFactories.Remove(reportFinalFactory);
        }

        public async Task<PagedList<ReportFinalFactoryDto>> GetAll(ReportFinalFactoryParams reportFinalFactoryParam)
        {
            var query = _context.ReportFinalFactories.AsQueryable();
            return await PagedList<ReportFinalFactoryDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ReportFinalFactoryDto>(_mapper.ConfigurationProvider),
                reportFinalFactoryParam.PageNumber,
                reportFinalFactoryParam.PageSize);
        }

        public async Task<List<ReportFinalFactory>> GetAll()
        {
            return await _context.ReportFinalFactories.ToListAsync();
        }

        public Task<ReportFinalFactory> GetByFactoryIdAndCreateDate(int factoryId, DateOnly createDate)
        {
            return _context.ReportFinalFactories.FirstOrDefaultAsync(x => x.FactoryId == factoryId && x.CreateDate == createDate)!;
        }

        public async Task<ReportFinalFactory> GetById(int id)
        {
            return (await _context.ReportFinalFactories.FindAsync(id))!;
        }

        public void Update(ReportFinalFactory reportFinalFactory)
        {
            _context.ReportFinalFactories.Update(reportFinalFactory);
        }
    }
}
