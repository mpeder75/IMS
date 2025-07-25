using IMS.Entities;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public class ViewInventoryByIdUseCase : IViewInventoryByIdUseCase
{
    public readonly IInventoryRepository _inventoryRepository;

    public ViewInventoryByIdUseCase(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Inventory> ExecuteAsync(int inventoryId)
    {
        return await this._inventoryRepository.GetInventoryByIdAsync(inventoryId);
    }
}