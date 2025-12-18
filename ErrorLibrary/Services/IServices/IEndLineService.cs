using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IEndLineService
    {
        Task<PagedList<EndLineDto>> GetAll(EndLineParams endLineParam);
        Task<List<EndLine>> GetAll();
        Task<EndLine> GetById(int id);
        void Add(EndLine endLine);
        void Update(EndLine endLine);
        void Delete(EndLine endLine);

        Task<bool> CheckExists(int lineId, int productId, DateOnly date);
        HashSet<string> BuildExistingEndLineKeySet(List<EndLine> endLines);
        bool CheckNameExistsFast(HashSet<string> existingKeys, int lineId, int productId, DateOnly date);
    }
}
