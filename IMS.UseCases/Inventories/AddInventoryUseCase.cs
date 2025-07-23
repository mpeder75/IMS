using IMS.Entities;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class AddInventoryUseCase : IAddInventoryUseCase
{
    // Dependency inject Inventory Repository
    private readonly IInventoryRepository _inventoryRepository;

    public AddInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    // Her til formål at tage imod en Inventory og lægge den i database
    public async Task ExecuteAsync(Inventory inventory)
    {
        await _inventoryRepository.AddInventoryAsync(inventory);
    }
}