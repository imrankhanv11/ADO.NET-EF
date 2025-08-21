using DBFirst_EFCore_01.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DBFirst_EFCore_01.Models;

public partial class StudentContext : DbContext
{
    public StudentContext()
    {
    }

    public StudentContext(DbContextOptions<StudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }

    public virtual DbSet<EmployeesProject> EmployeesProjects { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<VwEmployeeDepartment> VwEmployeeDepartments { get; set; }

    public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

    public virtual DbSet<empCount> EmployeeCounts { get; set; }

    public virtual DbSet<EmpWithDepartment> EmpWithDepartments { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=BSD-IMRANV01\\SQLEXPRESS;Database=StudentsEFCore_DBFirst;Trusted_Connection=True;TrustServerCertificate=True;");

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<EmployeeDepartment>().HasNoKey();

        modelBuilder.Entity<empCount>().HasNoKey();

        modelBuilder.Entity<EmpWithDepartment>().HasNoKey();

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benefits__3213E83F59479435");

            entity.ToTable("benefits");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BenefitName).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.Locations).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.DepartmentId, "IX_Employee_DepartmentID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<EmployeeBenefit>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.BenefitId }).HasName("PK__employee__1A58F7F43779C478");

            entity.ToTable("employeeBenefits");

            entity.HasOne(d => d.Benefit).WithMany(p => p.EmployeeBenefits)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employeeB__Benef__778AC167");

            entity.HasOne(d => d.Emp).WithMany(p => p.EmployeeBenefits)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employeeB__EmpId__76969D2E");
        });

        modelBuilder.Entity<EmployeesProject>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.ProjectId });

            entity.HasIndex(e => e.EmployeesId, "IX_EmployeesProjects_EmployeesID");

            entity.HasIndex(e => e.ProjectsId, "IX_EmployeesProjects_ProjectsId");

            entity.Property(e => e.EmpId).HasColumnName("EmpID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.Roll).HasMaxLength(50);

            entity.HasOne(d => d.Employees).WithMany(p => p.EmployeesProjects).HasForeignKey(d => d.EmployeesId);

            entity.HasOne(d => d.Projects).WithMany(p => p.EmployeesProjects).HasForeignKey(d => d.ProjectsId);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.ProjectName).HasMaxLength(80);
        });

        modelBuilder.Entity<VwEmployeeDepartment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwEmployeeDepartment");

            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
