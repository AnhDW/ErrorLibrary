using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface ILineService
    {
        Task<PagedList<LineDto>> GetAll(LineParam lineParam);
        Task<List<Line>> GetAll();
        Task<List<Line>> GetAllByEnterpriseId(int enterpriseId);
        Task<Line> GetById(int id);
        void Add(Line line);
        void Update(Line line);
        void Delete(Line line);
        Task<bool> CheckNameExists(string name, int enterpriseId);
    }
}
