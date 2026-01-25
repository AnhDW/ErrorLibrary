using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IReportFinalFactoryDetailService
    {
        Task<PagedList<ReportFinalFactoryDetailDto>> GetAll(ReportFinalFactoryDetailParams reportFinalFactoryDetailParam);
        Task<List<ReportFinalFactoryDetail>> GetAll();
        Task<List<ReportFinalFactoryDetailDisplayDto>> GetByReportFinalFactoryId(int reportFinalFactoryId);
        Task<ReportFinalFactoryDetail> GetById(int id);
        void Add(ReportFinalFactoryDetail reportFinalFactoryDetail);
        void Update(ReportFinalFactoryDetail reportFinalFactoryDetail);
        void Delete(ReportFinalFactoryDetail reportFinalFactoryDetail);
    }
}
