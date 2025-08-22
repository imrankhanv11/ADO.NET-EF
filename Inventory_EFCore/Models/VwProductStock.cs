using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class VwProductStock
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public int WarehouseId { get; set; }

    public string WarehouseName { get; set; } = null!;

    public int QuantityInStock { get; set; }
}
