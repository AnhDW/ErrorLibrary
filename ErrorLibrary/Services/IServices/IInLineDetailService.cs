using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IInLineDetailService
    {
        Task<PagedList<InLineDetailDisplayDto>> GetAll(InLineDetailParams inLineDetailParams);
        Task<List<InLineDetail>> GetAll();
        Task<InLineDetail> GetById(int id);
        void Add(InLineDetail inLineDetail);
        void Update(InLineDetail inLineDetail);
        void Delete(InLineDetail inLineDetail);

        Task<bool> CheckExists(int inLineId, int timeFrameId, int errorId);
    }
}
