using System;
using System.Collections.Generic;

namespace EXC_NorthWind_01_09_2025.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string RegionDescription { get; set; } = null!;

    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
