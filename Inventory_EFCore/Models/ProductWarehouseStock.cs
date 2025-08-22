using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class ProductWarehouseStock
{
    public int PwsId { get; set; }

    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int QuantityInStock { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
