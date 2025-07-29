using IMS.Entities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class ProductRepository : IProductRepository
{
    private List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>
        {
            new() { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150 },
            new() { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 2500 }
        };
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);

        return _products.Where
            (i => i.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public Task AddProductAsync(Product product)
    {
        // Check hvis product findes i forvejen, hvis den gør returneres den 
        if (_products.Any(x => x.ProductName.Equals(product.ProductName,
                StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        // Hvis product ikke findes, så får den et Id, og tilføjes til Listen
        var maxId = _products.Max(i => i.ProductId);
        product.ProductId = maxId + 1;

        _products.Add(product);

        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductId != product.ProductId &&
                                  x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }
        
        // Find product i listen
        var productToUpdate = _products
            .FirstOrDefault(x => x.ProductId == product.ProductId);

        // Hvis product findes(altså den ikker er null), så opdateres dets værdier
        if (productToUpdate is not null)
        {
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Price = product.Price;
        }

        return Task.CompletedTask;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    { 
       return await Task.FromResult(_products.FirstOrDefault(x => x.ProductId == id));
    }

    public async Task DeleteProductAsync(int productId)
    {
        // Find Product i listen
        var productToDelete = _products.FirstOrDefault(x => x.ProductId == productId);

        if (productToDelete is not null)
        {
            _products.Remove(productToDelete);
        }
        await Task.CompletedTask;
    }
}