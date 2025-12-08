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
        Task<List<TimeFrameColor>> GetByTimeFrame(int timeFrameId);
        Task<TimeFrameColor> GetById(int id);
        Task<List<TimeFrameColor>> GetByIds(List<int> ids);
        void Add(TimeFrameColor timeFrameColor);
        void Update(TimeFrameColor timeFrameColor);
        void Delete(TimeFrameColor timeFrameColor);

        void AddRange(List<TimeFrameColor> timeFrameColors);
        void UpdateRange(List<TimeFrameColor> timeFrameColors);
        void DeleteRange(List<TimeFrameColor> timeFrameColors);

        Task<bool> CheckExists(int timeFrameId, string hexCode);
    }
}
