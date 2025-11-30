using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorCategoryService
    {
        Task<PagedList<ErrorCategoryDto>> GetAll(ErrorCategoryParams errorCategoryParams);
        Task<List<ErrorCategory>> GetAll();
        Task<List<ErrorCategory>> GetByNames(List<string> names);
        Task<ErrorCategory> GetById(int id);
        void Add(ErrorCategory errorCategory);
        void Update(ErrorCategory errorCategory);
        void Delete(ErrorCategory errorCategory);
        Task<int> GetIdByName(string name);
        Task<bool> CheckNameExists(string name);
    }
}
