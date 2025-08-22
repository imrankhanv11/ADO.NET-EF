using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class InventoryLog
{
    public int LogId { get; set; }

    public int? ProductId { get; set; }

    public int? WarehouseId { get; set; }

    public string? Action { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? ActionDate { get; set; }

    public int? ReferenceId { get; set; }

    public int? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
}
