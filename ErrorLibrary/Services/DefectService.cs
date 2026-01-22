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
    public class DefectService : IDefectService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DefectService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Defect defect)
        {
            _context.Defects.Add(defect);
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.Defects.AnyAsync(x => x.Code == code);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.Defects.AnyAsync(x => x.Name == name);
        }

        public void Delete(Defect defect)
        {
            _context.Defects.Remove(defect);
        }

        public async Task<PagedList<DefectDto>> GetAll(DefectParams defectParam)
        {
            var query = _context.Defects.AsQueryable();
            return await PagedList<DefectDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<DefectDto>(_mapper.ConfigurationProvider),
                defectParam.PageNumber,
                defectParam.PageSize);
        }

        public async Task<List<Defect>> GetAll()
        {
            return await _context.Defects.ToListAsync();
        }

        public async Task<Defect> GetById(int id)
        {
            return (await _context.Defects.FindAsync(id))!;
        }

        public void Update(Defect defect)
        {
            _context.Defects.Update(defect);
        }
    }
}
