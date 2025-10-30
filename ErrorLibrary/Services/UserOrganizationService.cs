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
    public class UserOrganizationService : IUserOrganizationService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserOrganizationService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(UserOrganization userOrganization)
        {
            _context.UserOrganizations.Add(userOrganization);
        }

        public void Delete(UserOrganization userOrganization)
        {
            _context.UserOrganizations.Remove(userOrganization);
        }

        public Task<PagedList<UserOrganizationDto>> GetAll(UserOrganizationParams userOrganizationParams)
        {
            var query = _context.UserOrganizations.AsQueryable();

            return PagedList<UserOrganizationDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<UserOrganizationDto>(_mapper.ConfigurationProvider),
                userOrganizationParams.PageNumber,
                userOrganizationParams.PageSize);
        }

        public async Task<List<UserOrganization>> GetAll()
        {
            return await _context.UserOrganizations.ToListAsync();
        }

        public async Task<List<string>> GetUserIdsByOrganizationId(string organizationType, int organizationId)
        {
            return await _context.UserOrganizations
                .Where(x => x.OrganizationType == organizationType && x.OrganizationId == organizationId)
                .Select(x => x.UserId).ToListAsync();
        }

        public async Task<List<(string organizationType, int organizationId)>> GetOrganizationIdsByUserId(string userId)
        {
            var result = await _context.UserOrganizations.Where(x => x.UserId == userId)
                .Select(x => new { x.OrganizationType, x.OrganizationId }).ToListAsync();
            return result.Select(x => (x.OrganizationType, x.OrganizationId)).ToList();
        }

        public void Update(UserOrganization userOrganization)
        {
            _context.UserOrganizations.Update(userOrganization);
        }

        public async Task<UserOrganization> GetById(string userId, string organizationType, int organizationId)
        {
            return (await _context.UserOrganizations.FindAsync(userId, organizationType, organizationId))!;
        }
    }
}
