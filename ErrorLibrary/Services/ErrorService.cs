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
    public class ErrorService : IErrorService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Error error)
        {
            _context.Errors.Add(error);
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.Errors.AnyAsync(x => x.Code == code);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.Errors.AnyAsync(y => y.Name == name);
        }

        public void Delete(Error error)
        {
            _context.Errors.Remove(error);
        }

        public async Task<PagedList<ErrorDto>> GetAll(ErrorParams errorParams)
        {
            var query = _context.Errors.AsQueryable();
            return await PagedList<ErrorDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ErrorDto>(_mapper.ConfigurationProvider),
                errorParams.PageNumber,
                errorParams.PageSize);
        }

        public async Task<List<Error>> GetAll()
        {
            return await _context.Errors
                .Include(x => x.ErrorGroup)
                .Include(x => x.ErrorCategory)
                .Include(x => x.ProductCategory)
                .ToListAsync();
        }

        public async Task<Error> GetById(int id)
        {
            return (await _context.Errors.FirstOrDefaultAsync(x=>x.Id==id))!;
        }

        public void Update(Error error)
        {
            _context.Errors.Update(error);
        }
    }
}
