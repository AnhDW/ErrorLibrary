using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorDetailService
    {
        Task<PagedList<ErrorDetailDto>> GetAll(ErrorDetailParams errorDetailParams);
        Task<List<ErrorDetail>> GetAll();
        Task<ErrorDetail> GetById(int lineId, int productId, int errorId, string userId);
        void Add(ErrorDetail errorDetail);
        void Update(ErrorDetail errorDetail);
        void Delete(ErrorDetail errorDetail);
    }
}
