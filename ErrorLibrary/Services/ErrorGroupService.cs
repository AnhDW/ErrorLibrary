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
    public class ErrorGroupService : IErrorGroupService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorGroupService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ErrorGroup errorCategory)
        {
            _context.ErrorGroups.Add(errorCategory);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.ErrorGroups.AnyAsync(x => x.Name == name);
        }

        public void Delete(ErrorGroup errorCategory)
        {
            _context.ErrorGroups.Remove(errorCategory);
        }

        public async Task<PagedList<ErrorGroupDto>> GetAll(ErrorGroupParams errorCategoryParams)
        {
            var query = _context.ErrorGroups.AsQueryable();
            return await PagedList<ErrorGroupDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ErrorGroupDto>(_mapper.ConfigurationProvider),
                errorCategoryParams.PageNumber,
                errorCategoryParams.PageSize);
        }

        public async Task<List<ErrorGroup>> GetAll()
        {
            return await _context.ErrorGroups.ToListAsync();
        }

        public async Task<ErrorGroup> GetById(int id)
        {
            return (await _context.ErrorGroups.FindAsync(id))!;
        }

        public void Update(ErrorGroup errorCategory)
        {
            _context.ErrorGroups.Update(errorCategory);
        }
    }
}
