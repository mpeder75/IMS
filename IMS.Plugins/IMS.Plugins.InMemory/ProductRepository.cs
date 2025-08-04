using IMS.Entities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products;

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
        if (_products.Any(x => x.ProductId != product.ProductId && x.ProductName.ToLower() == product.ProductName.ToLower()))
        {
           return Task.CompletedTask;
        }

        // Find product i listen
        var productToUpdate = _products.FirstOrDefault(x => x.ProductId == product.ProductId);

        // Hvis product findes(altså den ikker er null), så opdateres dets værdier
        if (productToUpdate is not null)
        {
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Price = product.Price;
            productToUpdate.ProductInventories = product.ProductInventories;
        }
        return Task.CompletedTask;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var prod = _products.FirstOrDefault(x => x.ProductId == id);

        var newProd = new Product();

        if (prod != null)
        {
            newProd.ProductId = prod.ProductId;
            newProd.ProductName = prod.ProductName;
            newProd.Quantity = prod.Quantity;
            newProd.Price = prod.Price;
            newProd.ProductInventories = new List<ProductInventory>();

            if (prod.ProductInventories != null && prod.ProductInventories.Count > 0)
                foreach (var prodInv in prod.ProductInventories)
                {
                    var newProdInv = new ProductInventory
                    {
                        InventoryId = prodInv.InventoryId,
                        ProductId = prodInv.ProductId,
                        Product = prod,
                        Inventory = new Inventory(),
                        InventoryQuantity = prodInv.InventoryQuantity
                    };

                    if (prodInv.Inventory != null)
                    {
                        newProdInv.Inventory.InventoryId = prodInv.Inventory.InventoryId;
                        newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                        newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                        newProdInv.Inventory.Price = prodInv.Inventory.Price;
                    }

                    newProd.ProductInventories.Add(newProdInv);
                }
        }
        return await Task.FromResult(newProd);
    }

    public async Task DeleteProductByIdAsync(int productId)
    {
        // Find Product i listen
        var productToDelete = _products.FirstOrDefault(x => x.ProductId == productId);

        if (productToDelete is not null) _products.Remove(productToDelete);
        await Task.CompletedTask;
    }
}