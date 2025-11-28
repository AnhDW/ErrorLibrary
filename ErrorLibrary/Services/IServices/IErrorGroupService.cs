using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorGroupService
    {
        Task<PagedList<ErrorGroupDto>> GetAll(ErrorGroupParams errorGroupParams);
        Task<List<ErrorGroup>> GetAll();
        Task<ErrorGroup> GetById(int id);
        void Add(ErrorGroup errorGroup);
        void Update(ErrorGroup errorGroup);
        void Delete(ErrorGroup errorGroup);
        string GetLetterFromNumber(int number);
        string GetNextErrorGroupCode(List<string> existingCodes);
        Task<List<string>> GetAllCodes();
        Task<int> Count();
        Task<bool> CheckNameExists(string name);
        Task<bool> CheckCodeExists(string code);
    }
}
