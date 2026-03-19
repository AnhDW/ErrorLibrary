using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface ICustomerService
    {
        Task<PagedList<CustomerDto>> GetAll(CustomerParams customerParam);
        Task<List<Customer>> GetAll();
        Task<List<Customer>> GetByCodes(List<string> codes);
        Task<Customer> GetById(int id);
        Task<Customer> GetByCode(string code);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<bool> CheckNameExists(string name);
        Task<bool> CheckCodeExists(string code);
    }
}
