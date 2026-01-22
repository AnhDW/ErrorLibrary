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
    public class InspectionService : IInspectionService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InspectionService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
        }

        public void Delete(Inspection inspection)
        {
            _context.Inspections.Remove(inspection);
        }

        public async Task<PagedList<InspectionDto>> GetAll(InspectionParams inspectionParam)
        {
            var query = _context.Inspections.AsQueryable();
            return await PagedList<InspectionDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<InspectionDto>(_mapper.ConfigurationProvider),
                inspectionParam.PageNumber,
                inspectionParam.PageSize);
        }

        public async Task<List<Inspection>> GetAll()
        {
            return await _context.Inspections.ToListAsync();
        }

        public async Task<Inspection> GetById(int id)
        {
            return (await _context.Inspections.FindAsync(id))!;
        }

        public void Update(Inspection inspection)
        {
            _context.Inspections.Update(inspection);
        }
    }
}
