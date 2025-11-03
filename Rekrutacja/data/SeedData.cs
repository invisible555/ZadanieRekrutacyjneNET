using Microsoft.EntityFrameworkCore;
using VacationManagementSystem.Models;

namespace VacationManagementSystem.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Employees.Any())
                return;

         
            var team1 = new Team { Name = "Zespół IT" };
            var team2 = new Team { Name = "Zespół HR" };
            var team3 = new Team { Name = "Zespół Finansowy" };
            var team4 = new Team { Name = "Zespół Marketingu" };

           
            var package1 = new VacationPackage { Year = DateTime.Now.Year, GrantedDays = 26 };
            var package2 = new VacationPackage { Year = DateTime.Now.Year, GrantedDays = 20 };

         
            var jan = new Employee { Name = "Jan Kowalski", Team = team1, VacationPackage = package1 };
            var anna = new Employee { Name = "Anna Mariacka", Team = team1, VacationPackage = package1 };
            var kamil = new Employee { Name = "Kamil Nowak", Team = team2, VacationPackage = package2 };
            var ewa = new Employee { Name = "Ewa Król", Team = team2, VacationPackage = package1 };
            var piotr = new Employee { Name = "Piotr Zalewski", Team = team3, VacationPackage = package1 };
            var ola = new Employee { Name = "Aleksandra Wójcik", Team = team3, VacationPackage = package2 };
            var michal = new Employee { Name = "Michał Zieliński", Team = team4, VacationPackage = package2 };
            var karolina = new Employee { Name = "Karolina Górska", Team = team4, VacationPackage = package1 };

            var vacations = new List<Vacation>
            {
            
                new Vacation
                {
                    Employee = jan,
                    DateSince = DateTime.Now.AddDays(-30),
                    DateUntil = DateTime.Now.AddDays(-25)
                },

              
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-60),
                    DateUntil = DateTime.Now.AddDays(-50)
                },
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-40),
                    DateUntil = DateTime.Now.AddDays(-35)
                },
                new Vacation
                {
                    Employee = anna,
                    DateSince = DateTime.Now.AddDays(-20),
                    DateUntil = DateTime.Now.AddDays(-12)
                },

          
                new Vacation
                {
                    Employee = kamil,
                    DateSince = DateTime.Now.AddDays(-10),
                    DateUntil = DateTime.Now.AddDays(-6)
                },

              
                new Vacation
                {
                    Employee = ewa,
                    DateSince = DateTime.Now.AddDays(-45),
                    DateUntil = DateTime.Now.AddDays(-39)
                },
                new Vacation
                {
                    Employee = ewa,
                    DateSince = DateTime.Now.AddDays(-20),
                    DateUntil = DateTime.Now.AddDays(-18)
                },

              
                new Vacation
                {
                    Employee = piotr,
                    DateSince = new DateTime(2019, 6, 10),
                    DateUntil = new DateTime(2019, 6, 20)
                },

              
                new Vacation
                {
                    Employee = michal,
                    DateSince = DateTime.Now.AddDays(-7),
                    DateUntil = DateTime.Now.AddDays(-5)
                },

            
                new Vacation
                {
                    Employee = karolina,
                    DateSince = DateTime.Now.AddDays(-90),
                    DateUntil = DateTime.Now.AddDays(-85)
                },
                new Vacation
                {
                    Employee = karolina,
                    DateSince = DateTime.Now.AddDays(-10),
                    DateUntil = DateTime.Now.AddDays(-6)
                }
            };

       
            context.Teams.AddRange(team1, team2, team3, team4);
            context.VacationPackages.AddRange(package1, package2);
            context.Employees.AddRange(jan, anna, kamil, ewa, piotr, ola, michal, karolina);
            context.Vacations.AddRange(vacations);

            context.SaveChanges();
        }
    }
}
