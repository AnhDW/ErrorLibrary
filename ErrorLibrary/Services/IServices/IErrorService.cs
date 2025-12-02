using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorService
    {
        Task<PagedList<ErrorDisplayDto>> GetAll(ErrorParams errorParams);
        Task<List<Error>> GetAll();
        Task<Error> GetById(int id);
        void Add(Error error);
        void Update(Error error);
        void Delete(Error error);
        void AddRange(List<Error> errors);
        void UpdateRange(List<Error> errors);
        void DeleteRange(List<Error> errors);
        string GetNextErrorCode(string errorGroupCode, List<string> existingCodes);
        Task<List<string>> GetAllCodesByErrorGroupId(int errorGroupId);
        Task<bool> CheckNameExists(int errorGroupId, int errorCategoryId, int productCategoryId, string name);
        Task<bool> CheckCodeExists(string code);


        HashSet<string> BuildExistingErrorKeySet(List<Error> errors);
        bool CheckNameExistsFast(HashSet<string> existingKeys, int errorGroupId, int errorCategoryId, int productCategoryId, string name);
        
    }
}
