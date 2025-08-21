using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class Benefit
{
    public int Id { get; set; }

    public string BenefitName { get; set; } = null!;

    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
}
