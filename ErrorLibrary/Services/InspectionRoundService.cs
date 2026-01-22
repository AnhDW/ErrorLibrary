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
    public class InspectionRoundService : IInspectionRoundService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InspectionRoundService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(InspectionRound inspectionRound)
        {
            _context.InspectionRounds.Add(inspectionRound);
        }

        public void Delete(InspectionRound inspectionRound)
        {
            _context.InspectionRounds.Remove(inspectionRound);
        }

        public async Task<PagedList<InspectionRoundDto>> GetAll(InspectionRoundParams inspectionRoundParam)
        {
            var query = _context.InspectionRounds.AsQueryable();
            return await PagedList<InspectionRoundDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<InspectionRoundDto>(_mapper.ConfigurationProvider),
                inspectionRoundParam.PageNumber,
                inspectionRoundParam.PageSize);
        }

        public async Task<List<InspectionRound>> GetAll()
        {
            return await _context.InspectionRounds.ToListAsync();
        }

        public async Task<InspectionRound> GetById(int id)
        {
            return (await _context.InspectionRounds.FindAsync(id))!;
        }

        public void Update(InspectionRound inspectionRound)
        {
            _context.InspectionRounds.Update(inspectionRound);
        }
    }
}
