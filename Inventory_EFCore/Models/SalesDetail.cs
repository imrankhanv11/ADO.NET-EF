using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class SalesDetail
{
    public int SaleDetailId { get; set; }

    public int? SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Sale? Sale { get; set; }
}
