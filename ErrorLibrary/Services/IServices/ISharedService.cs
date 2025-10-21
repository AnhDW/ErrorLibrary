namespace ErrorLibrary.Services.IServices
{
    public interface ISharedService
    {
        Task<bool> SaveAllChanges();
    }
}
