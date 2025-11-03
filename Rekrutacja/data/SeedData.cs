using Microsoft.EntityFrameworkCore;
using VacationManagementSystem.Models;

namespace VacationManagementSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // 🔹 Jeśli dane już są — nie seedujemy ponownie
            if (context.Employees.Any())
                return;

            // 🔹 Zespoły
            var team1 = new Team { Name = "Zespół IT" };
            var team2 = new Team { Name = "Zespół HR" };

            // 🔹 Pakiety urlopowe
            var package1 = new VacationPackage { Year = DateTime.Now.Year, GrantedDays = 26 };
            var package2 = new VacationPackage { Year = DateTime.Now.Year, GrantedDays = 20 };

            // 🔹 Pracownicy
            var jan = new Employee { Name = "Jan Kowalski", Team = team1, VacationPackage = package1 };
            var anna = new Employee { Name = "Anna Mariacka", Team = team1, VacationPackage = package1 };
            var kamil = new Employee { Name = "Kamil Nowak", Team = team2, VacationPackage = package2 };

            // 🔹 Urlopy
            var vacations = new List<Vacation>
            {
                // 🟢 Jan – 6 dni urlopu
                new Vacation
                {
                    Employee = jan,
                    DateSince = DateTime.Now.AddDays(-30),
                    DateUntil = DateTime.Now.AddDays(-25)
                },

                // 🔴 Anna – 3 urlopy razem 26 dni (pełny limit)
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-60),
                    DateUntil = DateTime.Now.AddDays(-50) // 11 dni (włącznie)
                },
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-40),
                    DateUntil = DateTime.Now.AddDays(-35) // 6 dni
                },
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-20),
                    DateUntil = DateTime.Now.AddDays(-12) // 9 dni
                },

                // 🟢 Kamil – 5 dni urlopu
                new Vacation
                {
                    Employee = kamil,
                    DateSince = DateTime.Now.AddDays(-10),
                    DateUntil = DateTime.Now.AddDays(-6)
                }
            };

            // 🔹 Zapis do bazy
            context.Teams.AddRange(team1, team2);
            context.VacationPackages.AddRange(package1, package2);
            context.Employees.AddRange(jan, anna, kamil);
            context.Vacations.AddRange(vacations);

            context.SaveChanges();
        }
    }
}

