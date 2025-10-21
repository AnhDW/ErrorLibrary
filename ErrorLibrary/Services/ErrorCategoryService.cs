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

        public void Update(ErrorCategory errorCategory)
        {
            _context.ErrorCategories.Update(errorCategory);
        }
    }
}
