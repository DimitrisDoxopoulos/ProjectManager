using System;
using System.Collections.Generic;
using ContactsAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Models;

public partial class ContactsAppContext : DbContext
{
    public ContactsAppContext()
    {
    }

    public ContactsAppContext(DbContextOptions<ContactsAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public override int SaveChanges()
    {
        AddTimeStamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimeStamps();
        return await base.SaveChangesAsync();
    }

        private void AddTimeStamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));
        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;
            if (entity.State == EntityState.Added)
            {
                ((BaseModel)entity.Entity).CreatedAt = now;
            }
            ((BaseModel)entity.Entity).UpdatedAt = now;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMPLOYEES__3214EC277F4F16A4");

            entity.ToTable("EMPLOYEES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.CompanyRole)
                .HasMaxLength(255)
                .HasColumnName("COMPANY_ROLE");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLOYEES__USER_I__398D8EEE");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROJECTS__3214EC27DBAF9ADA");

            entity.ToTable("PROJECTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("DEADLINE");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Projects)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PROJECTS__CONTACT_I__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PROJECTS__USER_ID__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC27619BB059");

            entity.ToTable("USERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("USERNAME");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
