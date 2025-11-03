using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using VacationManagementSystem.Data;

namespace VacationManagementSystem
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 📁 Pobierz aktualną ścieżkę projektu (tam, gdzie jest csproj)
            var basePath = Directory.GetCurrentDirectory();

            // 🔹 Spróbuj znaleźć appsettings.json w tej ścieżce
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            // 🔹 Pobierz connection string lub ustaw domyślny
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=VacationManagement;Trusted_Connection=True;TrustServerCertificate=True;";

            // 🔹 Skonfiguruj DbContext
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
