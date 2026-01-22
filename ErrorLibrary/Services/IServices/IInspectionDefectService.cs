using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IInspectionDefectService
    {
        Task<PagedList<InspectionDefectDto>> GetAll(InspectionDefectParams inspectionDefectParam);
        Task<List<InspectionDefect>> GetAll();
        Task<InspectionDefect> GetById(int id);
        void Add(InspectionDefect inspectionDefect);
        void Update(InspectionDefect inspectionDefect);
        void Delete(InspectionDefect inspectionDefect);
    }
}