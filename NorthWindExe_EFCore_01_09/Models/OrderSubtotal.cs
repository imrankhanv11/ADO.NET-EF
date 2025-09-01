using System;
using System.Collections.Generic;

namespace EXC_NorthWind_01_09_2025.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
