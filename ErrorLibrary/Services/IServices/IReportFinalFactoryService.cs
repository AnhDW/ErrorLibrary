using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IReportFinalFactoryService
    {
        Task<PagedList<ReportFinalFactoryDto>> GetAll(ReportFinalFactoryParams reportFinalFactoryParam);
        Task<List<ReportFinalFactory>> GetAll();
        Task<List<ReportFinalFactory>> GetAllByCreateDate(DateOnly createDate);
        Task<ReportFinalFactory> GetById(int id);
        Task<ReportFinalFactory> GetByFactoryIdAndCreateDate(int factoryId, DateOnly createDate);
        void Add(ReportFinalFactory reportFinalFactory);
        void Update(ReportFinalFactory reportFinalFactory);
        void Delete(ReportFinalFactory reportFinalFactory);

        Task<bool> CheckExists(int factoryId, DateOnly createDate);
    }
}
