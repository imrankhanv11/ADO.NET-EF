using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();

    public virtual ICollection<ProductWarehouseStock> ProductWarehouseStocks { get; set; } = new List<ProductWarehouseStock>();
}
