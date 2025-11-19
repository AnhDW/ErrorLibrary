using AutoMapper;
using ErrorLibrary.Data;
using ErrorLibrary.Entities;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Services
{
    public class ErrorDetailAttachmentService : IErrorDetailAttachmentService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorDetailAttachmentService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ErrorDetailAttachment errorDetailAttachment)
        {
            _context.ErrorDetailAttachments.Add(errorDetailAttachment);
        }

        public void Delete(ErrorDetailAttachment errorDetailAttachment)
        {
            _context.ErrorDetailAttachments.Remove(errorDetailAttachment);
        }

        public async Task<List<ErrorDetailAttachment>> GetAll()
        {
            return await _context.ErrorDetailAttachments.ToListAsync();
        }

        public async Task<List<ErrorDetailAttachment>> GetByErrorDetail(int lineId, int productId, int errorId, string userId)
        {
            return await _context.ErrorDetailAttachments
                .Where(x => x.LineId == lineId && x.ProductId == productId && x.ErrorId == errorId && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<ErrorDetailAttachment> GetByIdAsync(int id)
        {
            return (await _context.ErrorDetailAttachments.FindAsync(id))!;
        }

        public void Update(ErrorDetailAttachment errorDetailAttachment)
        {
            _context.ErrorDetailAttachments.Update(errorDetailAttachment);
        }
    }
}
