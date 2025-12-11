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
    public class EndLineService : IEndLineService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EndLineService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(EndLine endLine)
        {
            _context.EndLines.Add(endLine);
        }

        public void Delete(EndLine endLine)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<EndLineDto>> GetAll(EndLineParams endLineParam)
        {
            throw new NotImplementedException();
        }

        public Task<List<EndLine>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<EndLine> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EndLine endLine)
        {
            throw new NotImplementedException();
        }
    }
}
