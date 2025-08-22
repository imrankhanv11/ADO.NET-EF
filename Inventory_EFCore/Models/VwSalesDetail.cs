using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class VwSalesDetail
{
    public int SaleId { get; set; }

    public DateOnly? SaleDate { get; set; }

    public string? CustomerName { get; set; }

    public string? SoldBy { get; set; }

    public int? ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPrice { get; set; }
}
