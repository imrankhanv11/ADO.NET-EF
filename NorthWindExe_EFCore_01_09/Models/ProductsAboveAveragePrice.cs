using System;
using System.Collections.Generic;

namespace EXC_NorthWind_01_09_2025.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
