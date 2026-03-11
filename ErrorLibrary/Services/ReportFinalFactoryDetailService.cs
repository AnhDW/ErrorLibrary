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
    public class ReportFinalFactoryDetailService : IReportFinalFactoryDetailService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportFinalFactoryDetailService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ReportFinalFactoryDetail reportFinalFactoryDetail)
        {
            _context.ReportFinalFactoryDetails.Add(reportFinalFactoryDetail);
        }

        public void Delete(ReportFinalFactoryDetail reportFinalFactoryDetail)
        {
            _context.ReportFinalFactoryDetails.Remove(reportFinalFactoryDetail);
        }

        public async Task<PagedList<ReportFinalFactoryDetailDto>> GetAll(ReportFinalFactoryDetailParams reportFinalFactoryDetailParam)
        {
            var query = _context.ReportFinalFactoryDetails.AsQueryable();
            return await PagedList<ReportFinalFactoryDetailDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ReportFinalFactoryDetailDto>(_mapper.ConfigurationProvider),
                reportFinalFactoryDetailParam.PageNumber,
                reportFinalFactoryDetailParam.PageSize);
        }

        public async Task<List<ReportFinalFactoryDetail>> GetAll()
        {
            return await _context.ReportFinalFactoryDetails.ToListAsync();
        }

        public async Task<ReportFinalFactoryDetail> GetById(int id)
        {
            return (await _context.ReportFinalFactoryDetails.FindAsync(id))!;
        }

        public async Task<List<ReportFinalFactoryDetail>> GetByReportFinalFactoryId(int reportFinalFactoryId)
        {
            return await _context.ReportFinalFactoryDetails.AsNoTracking()
                .Where(rffd => rffd.ReportFinalFactoryId == reportFinalFactoryId)
                .ToListAsync();
        }

        public void Update(ReportFinalFactoryDetail reportFinalFactoryDetail)
        {
            _context.ReportFinalFactoryDetails.Update(reportFinalFactoryDetail);
        }
    }
}
