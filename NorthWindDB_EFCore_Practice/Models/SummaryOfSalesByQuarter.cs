using System;
using System.Collections.Generic;

namespace Excersice2_EFCoreNorthWind.Models;

public partial class SummaryOfSalesByQuarter
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
