using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorGroupService
    {
        Task<PagedList<ErrorCategoryDto>> GetAll(ErrorGroupParams errorCategoryParams);
        Task<List<ErrorGroup>> GetAll();
        Task<ErrorGroup> GetById(int id);
        void Add(ErrorGroup errorCategory);
        void Update(ErrorGroup errorCategory);
        void Delete(ErrorGroup errorCategory);
    }
}
