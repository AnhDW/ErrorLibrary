using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface ITimeFrameColorService
    {
        Task<PagedList<TimeFrameColorDto>> GetAll(TimeFrameColorParams timeFrameColorParams);
        Task<List<TimeFrameColor>> GetAll();
        Task<TimeFrameColor> GetById(int id);
        void Add(TimeFrameColor timeFrameColor);
        void Update(TimeFrameColor timeFrameColor);
        void Delete(TimeFrameColor timeFrameColor);
    }
}
