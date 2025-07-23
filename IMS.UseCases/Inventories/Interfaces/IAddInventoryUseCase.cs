using IMS.Entities;

namespace IMS.UseCases.Inventories.Interfaces;

public interface IAddInventoryUseCase
{
    Task ExecuteAsync(Inventory inventory);
}