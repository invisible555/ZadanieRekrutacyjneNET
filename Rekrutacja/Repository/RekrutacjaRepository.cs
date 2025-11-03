using Rekrutacja.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Rekrutacja.Repository
{
    public class RekrutacjaRepository
    {
        private readonly AppDbContext _context;
        public RekrutacjaRepository(AppDbContext context)
        {
            _context = context;
        }
        //Zadanie 2 a
        public List<Employee> GetWithAtleastOneVacation()
        {
            return _context.Employees.Where(e => e.Vacations.Any(x => x.DateSince.Year == 2019))
                .ToList();
        }
        //Zadanie 2 b
        public List<EmployeeVacationDaysDTO> GetVacationDaysInCurrentYear()
        {
            int currentYear = DateTime.Now.Year;
            var employees = _context.Employees.Select(
                e => new EmployeeVacationDaysDTO
                {
                    Employee = e,
                    UsedVacationDays = e.Vacations.Where(x => x.DateSince.Year == currentYear && x.DateUntil < DateTime.Now).Sum(v => EF.Functions.DateDiffDay(v.DateSince, v.DateUntil))

                }).ToList();

            return employees;
        }

        //Zadanie 2 c
        public List<Team> GetTeamsWithNoVacationIn2019()
        {
            var teams = _context.Teams.Where(t => t.Employees.All(e => e.Vacations.All(v => v.DateSince.Year != 2019 && v.DateUntil.Year != 2019)))
                .ToList();
            return teams;
        }
        public List<Employee> GetAllEmployeesWithVacations()
        {
            return _context.Employees
                .Include(e => e.Vacations)
                .Include(e => e.VacationPackage)
                .ToList();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees
                .Include(e => e.Vacations)
                .Include(e => e.VacationPackage)
                .FirstOrDefault(e => e.Id == id);
        }
    }
    public class EmployeeVacationDaysDTO
    {
        public Employee Employee { get; set; } = new Employee();
        public int UsedVacationDays { get; set; }
    }
}
