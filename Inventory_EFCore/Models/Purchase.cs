using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public decimal? TotalCost { get; set; }

    public int? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

    public virtual User? User { get; set; }
}
