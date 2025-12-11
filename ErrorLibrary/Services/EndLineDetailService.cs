using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class EndLineDetailService : IEndLineDetailService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EndLineDetailService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(EndLineDetail endLineDetail)
        {
            throw new NotImplementedException();
        }

        public void Delete(EndLineDetail endLineDetail)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<EndLineDetailDto>> GetAll(EndLineDetailParams endLineDetailParam)
        {
            throw new NotImplementedException();
        }

        public Task<List<EndLineDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<EndLineDetail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EndLineDetail endLineDetail)
        {
            throw new NotImplementedException();
        }
    }
}
