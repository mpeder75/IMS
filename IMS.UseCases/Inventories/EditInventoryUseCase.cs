using IMS.Entities;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class EditInvetoryUseCase : IEditInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    public EditInvetoryUseCase(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        await _inventoryRepository.UpdateInventoryAsync(inventory);
    }
}