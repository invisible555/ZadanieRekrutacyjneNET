using Microsoft.EntityFrameworkCore;
using VacationManagementSystem.Models;

namespace VacationManagementSystem
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationPackage> VacationPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Team (1) -> (*) Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Team)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 VacationPackage (1) -> (*) Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.VacationPackage)
                .WithMany(vp => vp.Employees)
                .HasForeignKey(e => e.VacationPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Employee (1) -> (*) Vacation
            modelBuilder.Entity<Vacation>()
                .HasOne(v => v.Employee)
                .WithMany(e => e.Vacations)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
