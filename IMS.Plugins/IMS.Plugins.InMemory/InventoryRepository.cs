using IMS.Entities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class InventoryRepository : IInventoryRepository
{
    public List<Inventory> _inventories;

    public InventoryRepository()
    {
        _inventories = new List<Inventory>
        {
            new() { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
            new() { InventoryId = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15 },
            new() { InventoryId = 3, InventoryName = "Bike Wheels", Quantity = 20, Price = 8 },
            new() { InventoryId = 4, InventoryName = "Bike Pedels", Quantity = 20, Price = 1 }
        };
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

        return _inventories.Where
            (i => i.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}