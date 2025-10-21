using AutoMapper;
using AutoMapper.QueryableExtensions;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using Microsoft.EntityFrameworkCore;
using ProductCategoryLibrary.Services.IServices;

namespace ErrorLibrary.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }

        public void Delete(ProductCategory productCategory)
        {
            _context.ProductCategories.Remove(productCategory);
        }

        public async Task<PagedList<ProductCategoryDto>> GetAll(ProductCategoryParams productCategoryParams)
        {
            var query = _context.ProductCategories.AsQueryable();
            return await PagedList<ProductCategoryDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ProductCategoryDto>(_mapper.ConfigurationProvider),
                productCategoryParams.PageNumber,
                productCategoryParams.PageSize);
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetById(int id)
        {
            return (await _context.ProductCategories.FindAsync(id))!;
        }

        public void Update(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
        }
    }
}
