using ErrorLibrary.Entities;

namespace ErrorLibrary.Services.IServices
{
    public interface IErrorDetailAttachmentService
    {
        Task<List<ErrorDetailAttachment>> GetAll();
        Task<List<ErrorDetailAttachment>> GetByErrorDetail(int lineId, int productId, int errorId, string userId);
        Task<ErrorDetailAttachment> GetById(int id);
        void Add(ErrorDetailAttachment errorDetailAttachment);
        void Update(ErrorDetailAttachment errorDetailAttachment);
        void Delete(ErrorDetailAttachment errorDetailAttachment);
    }
}
