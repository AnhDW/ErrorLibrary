using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IProductService
    {
        Task<PagedList<ProductDto>> GetAll(ProductParams productParams);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
