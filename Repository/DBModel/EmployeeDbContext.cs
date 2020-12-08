using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace IKFApp
{
    public partial class EmployeeDbContext : DbContext
    {
        private string dbConnection = string.Empty;
        public EmployeeDbContext(string dbConnectionString)
        {
            dbConnection = dbConnectionString;
        }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbConnection);
        }
        public virtual DbSet<EmployeeRecord> EmployeesRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EmployeeRecord>(entity =>
            {
                entity.ToTable("Employee", "dbo");

                entity.Property(e => e.Designation)
                            .IsRequired()
                            .HasMaxLength(100);
                entity.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                entity.Property(e => e.Skills)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DOB).HasColumnType("datetime2");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
