using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? SaleDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();

    public virtual User? User { get; set; }
}
