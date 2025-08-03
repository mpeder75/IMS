using IMS.Entities;

namespace IMS.UseCases.Products.Interfaces;

public interface IViewIProductByIdUseCase
{
    Task<Product> ExecuteAsync(int productId);
}