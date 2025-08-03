using IMS.Entities;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products;

public class AddProductUseCase : IAddProductUseCase
{
    // Dependency inject Product Repository
    private readonly IProductRepository _productRepository;

    public AddProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(Product product)
    {
        await _productRepository.AddProductAsync(product);
    }
}