using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IInspectionRoundService
    {
        Task<PagedList<InspectionRoundDto>> GetAll(InspectionRoundParams inspectionRoundParam);
        Task<List<InspectionRound>> GetAll();
        Task<InspectionRound> GetById(int id);
        void Add(InspectionRound inspectionRound);
        void Update(InspectionRound inspectionRound);
        void Delete(InspectionRound inspectionRound);
    }
}
