using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IDefectService
    {
        Task<PagedList<DefectDto>> GetAll(DefectParams defectParam);
        Task<List<Defect>> GetAll();
        Task<Defect> GetById(int id);
        void Add(Defect defect);
        void Update(Defect defect);
        void Delete(Defect defect);
        Task<bool> CheckNameExists(string name);
        Task<bool> CheckCodeExists(string code);
    }
}
