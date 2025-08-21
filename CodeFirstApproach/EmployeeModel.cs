using CodeFirstApproach.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace CodeFirstApproach
{
    public class EmployeeModel : DbContext
    {
        public EmployeeModel()
            : base("name=Employee")
        {
        }

        public DbSet<Employees> Employee {  get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeesProjects {  get; set; }

    }

}