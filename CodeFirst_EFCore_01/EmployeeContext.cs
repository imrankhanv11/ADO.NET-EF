using CodeFirst_EFCore_01.DTO;
using CodeFirstApproach.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_EFCore_01
{
    public class EmployeeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("MyDb");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Employees> Employees {  get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EmployeeProject> EmployeesProject { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<spEmployee> spEmployee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmpID, ep.ProjectID });

            modelBuilder.Entity<spEmployee>().HasNoKey();
        }
    }
}
