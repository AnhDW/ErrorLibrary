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
        Task<List<ErrorGroup>> GetByNames(List<string> names);
        Task<ErrorGroup> GetById(int id);
        Task<ErrorGroup> GetByName(string name);
        void Add(ErrorGroup errorGroup);
        void Update(ErrorGroup errorGroup);
        void Delete(ErrorGroup errorGroup);
        string GetLetterFromNumber(int number);
        string GetNextErrorGroupCode(List<string> existingCodes);
        Task<List<string>> GetAllCodes();
        Task<int> GetIdByName(string name);
        Task<bool> CheckNameExists(string name);
        Task<bool> CheckCodeExists(string code);
    }
}
