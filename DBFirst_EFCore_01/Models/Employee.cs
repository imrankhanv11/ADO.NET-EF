using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();

    public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; } = new List<EmployeesProject>();
}
