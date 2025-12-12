using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IEndLineDetailService
    {
        Task<PagedList<EndLineDetailDto>> GetAll(EndLineDetailParams endLineDetailParam);
        Task<List<EndLineDetail>> GetAll();
        Task<EndLineDetail> GetById(int id);
        void Add(EndLineDetail endLineDetail);
        void Update(EndLineDetail endLineDetail);
        void Delete(EndLineDetail endLineDetail);

        Task<bool> CheckExists(int endLineId, int errorId, string userId);

        HashSet<string> BuildExistingEndLineDetailKeySet(List<EndLineDetail> endLineDetails);
        bool CheckExists(HashSet<string> existingKeys, int endLineId, int errorId, string userId);
    }
}
