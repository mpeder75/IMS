using System.ComponentModel.DataAnnotations;

namespace IMS.Entities;

public class InventoryTransaction
{
    public int InventoryTransactionId { get; set; }
    public string PONumber { get; set; } = string.Empty;

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public int QuantityBefore { get; set; }

    [Required]
    public InventoryTransactionType ActivityType { get; set; }

    [Required]
    public int QuantityAfter { get; set; }

    public double UnitPrice { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public string PurchasedBy { get; set; } = string.Empty;

    // Navigation property
    public Inventory? Inventory { get; set; }
}