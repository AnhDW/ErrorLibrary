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
    public class CustomerService : ICustomerService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.Customers.AnyAsync(x => x.Code == code);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.Customers.AnyAsync(x => x.Name == name);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<PagedList<CustomerDto>> GetAll(CustomerParams customerParam)
        {
            var query = _context.Customers.AsQueryable();
            return await PagedList<CustomerDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<CustomerDto>(_mapper.ConfigurationProvider),
                customerParam.PageNumber,
                customerParam.PageSize);
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return (await _context.Customers.FindAsync(id))!;
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
