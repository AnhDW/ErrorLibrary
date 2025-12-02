using AutoMapper;
using AutoMapper.QueryableExtensions;
using ErrorLibrary.Data;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public void AddRange(List<Error> errors)
        {
            _context.Errors.AddRange(errors);
        }

        public HashSet<string> BuildExistingErrorKeySet(List<Error> errors)
        {
            return errors.Select(x => $"{x.ErrorGroupId}|{x.ErrorCategoryId}|{x.ProductCategoryId}|{x.Name}").ToHashSet();
        }

        public async Task<bool> CheckCodeExists(string code)
        {
            return await _context.Errors.AnyAsync(x => x.Code == code);
        }

        public async Task<bool> CheckNameExists(int errorGroupId, int errorCategoryId, int productCategoryId, string name)
        {
            return await _context.Errors.AnyAsync(x =>
                x.ErrorGroupId == errorGroupId &&
                x.ErrorCategoryId == errorCategoryId &&
                x.ProductCategoryId == productCategoryId &&
                x.Name == name
            );
        }

        public bool CheckNameExistsFast(HashSet<string> existingKeys, int errorGroupId, int errorCategoryId, int productCategoryId, string name)
        {
            var key = $"{errorGroupId}|{errorCategoryId}|{productCategoryId}|{name}";
            return existingKeys.Contains(key);
        }

        public void Delete(Error error)
        {
            _context.Errors.Remove(error);
        }

        public void DeleteRange(List<Error> errors)
        {
            _context.Errors.RemoveRange(errors);
        }

        public async Task<PagedList<ErrorDisplayDto>> GetAll(ErrorParams errorParams)
        {
            var query = await _context.Errors
                .Include(x => x.ErrorGroup)
                .Include(x => x.ErrorCategory)
                .Include(x => x.ProductCategory)
                .ToListAsync();

            query = query
                .OrderBy(x => Regex.Match(x.Code, @"^[A-Za-z]+").Value)
                .ThenBy(x => int.Parse(Regex.Match(x.Code, @"\d+").Value)).ToList();

            if (errorParams.ErrorGroupIds.Any())
            {
                query = query.Where(x => errorParams.ErrorGroupIds.Contains(x.ErrorGroupId)).ToList();
            }
            
            if (errorParams.ErrorCategoryIds.Any())
            {
                query = query.Where(x => errorParams.ErrorCategoryIds.Contains(x.ErrorCategoryId ?? -1)).ToList();
            }
            
            if (errorParams.ProductCategoryIds.Any())
            {
                query = query.Where(x => errorParams.ProductCategoryIds.Contains(x.ProductCategoryId)).ToList();
            }

            if (!string.IsNullOrEmpty(errorParams.Code))
            {
                query = query.Where(x => x.Code.Contains(errorParams.Code)).ToList();
            }

            if (!string.IsNullOrEmpty(errorParams.Name))
            {
                query = query.Where(x => x.Name.Contains(errorParams.Name)).ToList();
            }

            return await PagedList<ErrorDisplayDto>.CreateAsync(
                query.AsQueryable().AsNoTracking().ProjectTo<ErrorDisplayDto>(_mapper.ConfigurationProvider),
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

        public async Task<List<string>> GetAllCodesByErrorGroupId(int errorGroupId)
        {
            return await _context.Errors
                .Where(x => x.ErrorGroupId == errorGroupId)
                .Select(x => x.Code)
                .ToListAsync();
        }

        public async Task<Error> GetById(int id)
        {
            return (await _context.Errors.FirstOrDefaultAsync(x=>x.Id==id))!;
        }

        public string GetNextErrorCode(string errorGroupCode, List<string> existingCodes)
        {
            var numbers = existingCodes
                .Select(code => code.Substring(errorGroupCode.Length))
                .Where(num => int.TryParse(num, out _))
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToList();
            int nextNumber = 1;
            foreach (var n in numbers)
            {
                if (n == nextNumber) nextNumber++;
                else break; // gặp số bị hổng → lấy số đó
            }

            return $"{errorGroupCode}{nextNumber}";
        }

        public void Update(Error error)
        {
            _context.Errors.Update(error);
        }

        public void UpdateRange(List<Error> errors)
        {
            _context.Errors.UpdateRange(errors);
        }
    }
}
