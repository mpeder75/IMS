using IMS.Entities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    public List<InventoryTransaction> _inventoryTransactions = new();

    public void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string purchasedBy, double price)
    {
        _inventoryTransactions.Add(new InventoryTransaction
        {
            PONumber = poNumber,
            InventoryId = inventory.InventoryId,
            QuantityBefore = inventory.Quantity,
            ActivityType = InventoryTransactionType.PurchaseInventory,
            QuantityAfter = inventory.Quantity + quantity,
            UnitPrice = price,
            TransactionDate = DateTime.Now,
            PurchasedBy = purchasedBy
        });
    }
}