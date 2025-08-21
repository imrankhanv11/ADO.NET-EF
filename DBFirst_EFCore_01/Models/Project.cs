using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public int Budget { get; set; }

    public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; } = new List<EmployeesProject>();
}
