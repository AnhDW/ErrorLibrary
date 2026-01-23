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
    public class ReportFinalFactoryDetailDefectService : IReportFinalFactoryDetailDefectService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportFinalFactoryDetailDefectService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ReportFinalFactoryDetailDefect customer)
        {
            _context.ReportFinalFactoryDetailDefects.Add(customer);
        }

        public void Delete(ReportFinalFactoryDetailDefect customer)
        {
            _context.ReportFinalFactoryDetailDefects.Remove(customer);
        }

        public async Task<PagedList<ReportFinalFactoryDetailDefectDto>> GetAll(ReportFinalFactoryDetailDefectParams customerParam)
        {
            var query = _context.ReportFinalFactoryDetailDefects.AsQueryable();
            return await PagedList<ReportFinalFactoryDetailDefectDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ReportFinalFactoryDetailDefectDto>(_mapper.ConfigurationProvider),
                customerParam.PageNumber,
                customerParam.PageSize);
        }

        public async Task<List<ReportFinalFactoryDetailDefect>> GetAll()
        {
            return await _context.ReportFinalFactoryDetailDefects.ToListAsync();
        }

        public async Task<ReportFinalFactoryDetailDefect> GetById(int id)
        {
            return (await _context.ReportFinalFactoryDetailDefects.FindAsync(id))!;
        }

        public void Update(ReportFinalFactoryDetailDefect customer)
        {
            _context.ReportFinalFactoryDetailDefects.Update(customer);
        }
    }
}
