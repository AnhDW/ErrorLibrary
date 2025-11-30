using AutoMapper;
using AutoMapper.QueryableExtensions;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;

namespace ErrorLibrary.Services
{
    public class ErrorGroupService : IErrorGroupService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ErrorGroupService(IDbContextFactory<AppDbContext> dbContextFactory, AppDbContext context, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _context = context;
            _mapper = mapper;
        }

        public void Add(ErrorGroup errorGroup)
        {
            _context.ErrorGroups.Add(errorGroup);
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.ErrorGroups.AnyAsync(x => x.Code == code);
        }

        public async Task<bool> CheckNameExists(string name)
        {
            return await _context.ErrorGroups.AnyAsync(x => x.Name == name);
        }

        public void Delete(ErrorGroup errorGroup)
        {
            _context.ErrorGroups.Remove(errorGroup);
        }

        public async Task<PagedList<ErrorGroupDto>> GetAll(ErrorGroupParams errorGroupParams)
        {
            var query = _context.ErrorGroups.AsQueryable();
            return await PagedList<ErrorGroupDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<ErrorGroupDto>(_mapper.ConfigurationProvider),
                errorGroupParams.PageNumber,
                errorGroupParams.PageSize);
        }

        public async Task<List<ErrorGroup>> GetAll()
        {
            return await _context.ErrorGroups.ToListAsync();
        }

        public async Task<List<string>> GetAllCodes()
        {
            return await _context.ErrorGroups.Select(eg => eg.Code).ToListAsync();
        }

        public async Task<ErrorGroup> GetById(int id)
        {
            return (await _context.ErrorGroups.FindAsync(id))!;
        }

        public async Task<ErrorGroup> GetByName(string name)
        {
            return (await _context.ErrorGroups.FirstOrDefaultAsync(x => x.Name == name))!;
        }

        public async Task<List<ErrorGroup>> GetByNames(List<string> names)
        {
            return await _context.ErrorGroups.Where(x=> names.Contains(x.Name)).ToListAsync();
        }

        public async Task<int> GetIdByName(string name)
        {
            return await _context.ErrorGroups
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public string GetLetterFromNumber(int number)
        {
            string letters = "";
            while (number >= 0)
            {
                letters = (char)(number % 26 + 'A') + letters;
                number = number / 26 - 1;
            }
            return letters;
        }

        public string GetNextErrorGroupCode(List<string> existingCodes)
        {
            int index = 0;
            while (true)
            {
                string code = GetLetterFromNumber(index);
                if (!existingCodes.Contains(code))
                    return code;
                index++;
            }
        }

        public void Update(ErrorGroup errorGroup)
        {
            _context.ErrorGroups.Update(errorGroup);
        }
    }
}
