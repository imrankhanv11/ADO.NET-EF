using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class VwPurchaseDetail
{
    public int PurchaseId { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? PurchasedBy { get; set; }

    public int? ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal? TotalCost { get; set; }
}
