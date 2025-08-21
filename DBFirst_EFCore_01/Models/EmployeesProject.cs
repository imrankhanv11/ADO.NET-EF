using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class EmployeesProject
{
    public int EmpId { get; set; }

    public int ProjectId { get; set; }

    public string Roll { get; set; } = null!;

    public int EmployeesId { get; set; }

    public int ProjectsId { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual Project Projects { get; set; } = null!;
}
