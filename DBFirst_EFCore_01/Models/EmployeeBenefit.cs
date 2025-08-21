using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class EmployeeBenefit
{
    public int EmpId { get; set; }

    public int BenefitId { get; set; }

    public DateOnly? AddDate { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;
}
