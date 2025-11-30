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
    public class ErrorCategoryService : IErrorCategoryService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorCategoryService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ErrorCategory errorCategory)
        {
            _context.ErrorCategories.Add(errorCategory);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.ErrorCategories.AnyAsync(x => x.Name == name);
        }

        public void Delete(ErrorCategory errorCategory)
        {
            _context.ErrorCategories.Remove(errorCategory);
        }

        public async Task<PagedList<ErrorCategoryDto>> GetAll(ErrorCategoryParams errorCategoryParams)
        {
            var query = _context.ErrorCategories.AsQueryable();
            return await PagedList<ErrorCategoryDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ErrorCategoryDto>(_mapper.ConfigurationProvider),
                errorCategoryParams.PageNumber,
                errorCategoryParams.PageSize);
        }

        public async Task<List<ErrorCategory>> GetAll()
        {
            return await _context.ErrorCategories.ToListAsync();
        }

        public async Task<ErrorCategory> GetById(int id)
        {
            return (await _context.ErrorCategories.FindAsync(id))!;
        }

        public async Task<List<ErrorCategory>> GetByNames(List<string> names)
        {
            return await _context.ErrorCategories.Where(x => names.Contains(x.Name)).ToListAsync();
        }

        public async Task<int> GetIdByName(string name)
        {
            return await _context.ErrorCategories
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public void Update(ErrorCategory errorCategory)
        {
            _context.ErrorCategories.Update(errorCategory);
        }
    }
}
