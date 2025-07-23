using IMS.Entities;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoriesByAsync(string name);
        Task AddInventoryAsync(Inventory inventory);
    }
}
