using IMS.Entities;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities;

public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
{
    private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository, 
        IInventoryRepository inventoryRepository
    )
    {
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string purchasedBy)
    {
        // 1. insert a record in the transaction table
        _inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, purchasedBy, inventory.Price);

        // 2. update the inventory table
        inventory.Quantity += quantity;
        await _inventoryRepository.UpdateInventoryAsync(inventory);

    }
}