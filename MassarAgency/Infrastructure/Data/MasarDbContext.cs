using MassarAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MassarAgency.Infrastructure.Data;

public class MasarDbContext : DbContext
{
    public MasarDbContext(DbContextOptions<MasarDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<DeductionPolicy> DeductionPolicies => Set<DeductionPolicy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Department
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Description).HasMaxLength(500);
            entity.HasIndex(d => d.Name).IsUnique();
        });

        // Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();

            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(e => e.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Attendance
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Notes).HasMaxLength(500);
            entity.HasIndex(a => new { a.EmployeeId, a.Date }).IsUnique();

            entity.HasOne(a => a.Employee)
                  .WithMany(e => e.Attendances)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // DeductionPolicy
        modelBuilder.Entity<DeductionPolicy>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.DeductionPerAbsenceDay).HasColumnType("decimal(18,2)");
            entity.Property(p => p.DeductionPerLateDay).HasColumnType("decimal(18,2)");
            entity.Property(p => p.MaxMonthlyDeduction).HasColumnType("decimal(18,2)");
        });

        // Seed data
        SeedData.Seed(modelBuilder);
    }
}
