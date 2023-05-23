using Microsoft.EntityFrameworkCore;
using System;

namespace ShiftLogger.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shift> Shifts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shift>()
                .Property(p => p.Duration)
                .HasComputedColumnSql("CONVERT(TIME,CONVERT(DATETIME, [End]) - CONVERT(DATETIME, [Start]))");
        }
        //use MS SQL Server with connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\CSAcademyDB;Initial Catalog=ShiftLog;Integrated Security=True");
        }
        
    }
}
