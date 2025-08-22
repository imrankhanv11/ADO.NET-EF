using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityInStock { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<ProductWarehouseStock> ProductWarehouseStocks { get; set; } = new List<ProductWarehouseStock>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
