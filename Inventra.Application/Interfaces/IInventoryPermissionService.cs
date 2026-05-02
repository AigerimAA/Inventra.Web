namespace Inventra.Application.Interfaces
{
    public interface IInventoryPermissionService
    {
        Task<bool> CanWriteAsync(string userId, int inventoryId);
        Task<bool> CanManageAsync(string userId, int inevntoryId);
    }
}
