using VacationManagementSystem.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.EntityFrameworkCore;
using VacationManagementSystem.DTO;

namespace VacationManagementSystem.Repository
{
    public class VacationManagmentRepository
    {
        private readonly AppDbContext _context;
        public VacationManagmentRepository(AppDbContext context)
        {
            _context = context;
        }
        //Zadanie 2 a
        public List<Employee> GetWithAtleastOneVacation(int year)
        {
            return _context.Employees.Where(e => e.Vacations.Any(x => x.DateSince.Year == year))
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
        public List<Team> GetTeamsWithNoVacation(int year)
        {
            var teams = _context.Teams.Where(t => t.Employees.All(e => e.Vacations.All(v => v.DateSince.Year != year && v.DateUntil.Year != year)))
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
    
}
