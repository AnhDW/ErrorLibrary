using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface ITimeFrameService
    {
        Task<PagedList<TimeFrameDto>> GetAll(TimeFrameParams timeFrameParams);
        Task<List<TimeFrame>> GetAll();
        Task<TimeFrame> GetById(int id);
        void Add(TimeFrame timeFrame);
        void Update(TimeFrame timeFrame);
        void Delete(TimeFrame timeFrame);
    }
}
