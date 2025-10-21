using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface ISolutionService
    {
        Task<PagedList<SolutionDto>> GetAll(SolutionParams solutionParams);
        Task<List<Solution>> GetAll();
        Task<Solution> GetById(int id);
        void Add(Solution solution);
        void Update(Solution solution);
        void Delete(Solution solution);
    }
}
