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
        Task<ReportFinalFactoryDetailDefect> GetById(int id);
        void Add(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);
        void Update(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);
        void Delete(ReportFinalFactoryDetailDefect reportFinalFactoryDetailDefect);
    }
}
