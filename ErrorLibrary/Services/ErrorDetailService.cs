using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;
using ErrorLibrary.Services.IServices;

namespace ErrorLibrary.Services
{
    public class ErrorDetailService : IErrorDetailService
    {

        public void Add(ErrorDetail errorDetail)
        {
            throw new NotImplementedException();
        }

        public void Delete(ErrorDetail errorDetail)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ErrorDetailDto>> GetAll(ErrorDetailParams errorDetailParams)
        {
            throw new NotImplementedException();
        }

        public Task<List<ErrorDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ErrorDetail> GetById(int lineId, int productId, int errorId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Update(ErrorDetail errorDetail)
        {
            throw new NotImplementedException();
        }
    }
}
