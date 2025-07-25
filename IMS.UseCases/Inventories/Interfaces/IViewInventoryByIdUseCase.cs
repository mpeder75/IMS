using IMS.Entities;

namespace IMS.UseCases.Inventories.Interfaces;

public interface IViewInventoryByIdUseCase
{
    Task<Inventory> ExecuteAsync(int inventoryId);
}