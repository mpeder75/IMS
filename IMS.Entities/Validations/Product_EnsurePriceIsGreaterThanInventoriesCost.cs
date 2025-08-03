using System.ComponentModel.DataAnnotations;

namespace IMS.Entities.Validations;

public class Product_EnsurePriceIsGreaterThanInventoriesCost : ValidationAttribute
{
    // Udfører valideringen. Returnerer en fejl, hvis produktets pris er mindre end den samlede lagerkostpris.
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = validationContext.ObjectInstance as Product;

        if (product != null)
        {
            // Tjekker om prisen er mindre end lagerkostprisen
            if (!ValidatePricing(product))
            {
                return new ValidationResult(
                    $"The product price is less than the inventories cost: {TotalInventoriesCost(product).ToString("c")}",
                    new List<string>() { validationContext.MemberName });
            }
        }
        return ValidationResult.Success;
    }

   
    // Beregner den samlede kostpris for alle lagervarer tilknyttet produktet.
    private double TotalInventoriesCost(Product product)
    {
        if (product == null || product.ProductInventories == null) return 0;

        // Summerer prisen for hver lagervare ganget med antal
        return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
    }
    
    // Validerer om produktets pris er større end eller lig med den samlede lagerkostpris.
    // <param name="product">Produktet der skal valideres.</param>
    // <returns>True hvis prisen er gyldig, ellers false.</returns>
    private bool ValidatePricing(Product product)
    {
        if (product.ProductInventories == null || product.ProductInventories.Count <= 0) return true;

        if (TotalInventoriesCost(product) > product.Price) return false;

        return true;
    }
}
