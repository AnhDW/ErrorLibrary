using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IInspectionService
    {
        Task<PagedList<InspectionDto>> GetAll(InspectionParams inspectionParam);
        Task<List<Inspection>> GetAll();
        Task<Inspection> GetById(int id);
        void Add(Inspection inspection);
        void Update(Inspection inspection);
        void Delete(Inspection inspection);
    }
}
