using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAll();
        Task<List<PermissionTreeDto>> GetPermissionTreeAsync();
        Task<Permission> GetById(int id);
        void Add(Permission unit);
        void Update(Permission unit);
        void Delete(Permission unit);
    }
}
