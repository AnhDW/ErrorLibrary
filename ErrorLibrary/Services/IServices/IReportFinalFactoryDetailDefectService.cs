using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IReportFinalFactoryDetailDefectService
    {
        Task<PagedList<ReportFinalFactoryDetailDefectDto>> GetAll(ReportFinalFactoryDetailDefectParams reportFinalFactoryDetailDefectParams);
        Task<List<ReportFinalFactoryDetailDefect>> GetAll();
        Task<List<ReportFinalFactoryDetailDefect>> GetByReportFinalFactoryDetailId(int reportFinalFactoryDetailId);
        Task<ReportFinalFactoryDetailDefect> GetById(int reportFinalFactoryDetailId, int defectId);
        void Add(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);
        void Update(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);
        void Delete(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);

        void DeleteRange(List<ReportFinalFactoryDetailDefect> reportFinalFactoryDetailDefects);

        Task<bool> CheckExists(int reportFinalFactoryDetailId, int defectId);
    }
}
