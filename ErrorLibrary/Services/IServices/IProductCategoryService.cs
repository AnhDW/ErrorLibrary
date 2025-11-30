using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ProductCategoryLibrary.Services.IServices
{
    public interface IProductCategoryService
    {
        Task<PagedList<ProductCategoryDto>> GetAll(ProductCategoryParams productCategoryParams);
        Task<List<ProductCategory>> GetAll();
        Task<List<ProductCategory>> GetByNames(List<string> names);
        Task<ProductCategory> GetById(int id);
        void Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        void Delete(ProductCategory productCategory);
        Task<int> GetIdByName(string name);
        Task<bool> CheckNameExists(string name);
    }
}
