using IMS.Entities;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
      Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
      Task AddProductAsync(Product product);
      Task UpdateProductAsync(Product product);
      Task<Product?> GetProductByIdAsync(int id);
      Task DeleteProductByIdAsync(int productId);
    }
}
