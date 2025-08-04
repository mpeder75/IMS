using IMS.Entities;

namespace IMS.UseCases.Activities.Interfaces;

public interface IPurchaseInventoryUseCase
{
    Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string purchasedBy);
}