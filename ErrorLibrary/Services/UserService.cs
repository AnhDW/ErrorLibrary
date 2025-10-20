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
    public class UserService : IUserService
    {
        //private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(
            //IDbContextFactory<AppDbContext> dbContextFactory, 
            AppDbContext context, IMapper mapper)
        {
            //_dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
        }

        public void Delete(ApplicationUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<PagedList<UserDto>> GetAll(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            if (userParams.UserCode != null)
            {
                query = query.Where(x => x.Code.Contains(userParams.UserCode));
            }

            if (userParams.FullName != null)
            {
                query = query.Where(x => x.FullName.Contains(userParams.FullName));
            }

            return await PagedList<UserDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<UserDto>(_mapper.ConfigurationProvider),
                userParams.PageNumber,
                userParams.PageSize);
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByCode(string code)
        {
            return (await _context.Users.FirstOrDefaultAsync(x => x.Code == code))!;
        }

        public async Task<List<ApplicationUser>> GetByFullName(string fullName)
        {
            return await _context.Users.Where(x => x.FullName.Contains(fullName)).ToListAsync();
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return (await _context.Users.FindAsync(id))!;
        }

        public Task<List<ApplicationUser>> GetByRoleName(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationUser>> GetByUserIds(List<string> userIds)
        {
            return await _context.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
        }
    }
}
