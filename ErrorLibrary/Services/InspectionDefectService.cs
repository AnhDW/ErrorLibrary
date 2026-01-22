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
    public class InspectionDefectService : IInspectionDefectService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InspectionDefectService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(InspectionDefect inspectionDefect)
        {
            _context.InspectionDefects.Add(inspectionDefect);
        }

        public void Delete(InspectionDefect inspectionDefect)
        {
            _context.InspectionDefects.Remove(inspectionDefect);
        }

        public async Task<PagedList<InspectionDefectDto>> GetAll(InspectionDefectParams inspectionDefectParam)
        {
            var query = _context.InspectionDefects.AsQueryable();
            return await PagedList<InspectionDefectDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<InspectionDefectDto>(_mapper.ConfigurationProvider),
                inspectionDefectParam.PageNumber,
                inspectionDefectParam.PageSize);
        }

        public async Task<List<InspectionDefect>> GetAll()
        {
            return await _context.InspectionDefects.ToListAsync();
        }

        public async Task<InspectionDefect> GetById(int id)
        {
            return (await _context.InspectionDefects.FindAsync(id))!;
        }

        public void Update(InspectionDefect inspectionDefect)
        {
            _context.InspectionDefects.Update(inspectionDefect);
        }
    }
}
