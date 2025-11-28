using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorService
    {
        Task<PagedList<ErrorDto>> GetAll(ErrorParams errorParams);
        Task<List<Error>> GetAll();
        Task<Error> GetById(int id);
        void Add(Error error);
        void Update(Error error);
        void Delete(Error error);
        string GetNextErrorCode(string errorGroupCode, List<string> existingCodes);
        Task<List<string>> GetAllCodesByErrorGroupId(int errorGroupId);
        Task<bool> CheckNameExists(string name);
        Task<bool> CheckCodeExists(string code);
    }
}
