using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IStyleService
    {
        Task<PagedList<StyleDto>> GetAll(StyleParams styleParam);
        Task<List<Style>> GetAll();
        Task<Style> GetById(int id);
        Task<Style> GetByCode(string code);
        void Add(Style style);
        void Update(Style style);
        void Delete(Style style);
        Task<bool> CheckCodeExists(string code);
    }
}
