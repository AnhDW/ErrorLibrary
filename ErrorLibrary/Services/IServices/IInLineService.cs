using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IInLineService
    {
        Task<PagedList<InLineDto>> GetAll(InLineParams inLineParams);
        Task<List<InLine>> GetAll();
        Task<InLine> GetById(int id);
        void Add(InLine inLine);
        void Update(InLine inLine);
        void Delete(InLine inLine);

        Task<bool> CheckExists(int inLineId, int productId, string userId, DateOnly createDate);

        HashSet<string> BuildExistingErrorKeySet(List<InLine> inLines);
        bool CheckNameExistsFast(HashSet<string> existingKeys, int lineId, int productId, string userId, DateOnly createDate);

    }
}
