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
            return await _context.Errors.ToListAsync();
        }

        public async Task<Error> GetById(int id)
        {
            return (await _context.Errors.FindAsync(id))!;
        }

        public void Update(Error error)
        {
            _context.Errors.Update(error);
        }
    }
}
