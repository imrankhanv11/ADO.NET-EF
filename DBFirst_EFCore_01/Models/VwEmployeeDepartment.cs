using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class VwEmployeeDepartment
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;
}
