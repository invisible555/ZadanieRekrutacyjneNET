using VacationManagementSystem.Models;
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

        //  Zadanie 2a – Pracownicy z przynajmniej jednym urlopem w danym roku
        public async Task<List<Employee>> GetWithAtleastOneVacationAsync(int year)
        {
            return await _context.Employees
                .Where(e => e.Vacations.Any(v => v.DateSince.Year == year))
                .ToListAsync();
        }

        //  Zadanie 2b – Liczba dni urlopowych wykorzystanych w bieżącym roku
        public async Task<List<EmployeeVacationDaysDTO>> GetVacationDaysInCurrentYearAsync()
        {
            int currentYear = DateTime.Now.Year;

            return await _context.Employees
                .Select(e => new EmployeeVacationDaysDTO
                {
                    Employee = e,
                    UsedVacationDays = e.Vacations
                        .Where(v => v.DateSince.Year == currentYear && v.DateUntil < DateTime.Now)
                        .Sum(v => EF.Functions.DateDiffDay(v.DateSince, v.DateUntil))
                })
                .ToListAsync();
        }

        //  Zadanie 2c – Zespoły, których pracownicy nie mieli urlopu w danym roku
        public async Task<List<Team>> GetTeamsWithNoVacationAsync(int year)
        {
            return await _context.Teams
                .Where(t => t.Employees.All(e => e.Vacations.All(v => v.DateSince.Year != year && v.DateUntil.Year != year)))
                .ToListAsync();
        }

        //  Pobranie wszystkich pracowników z urlopami i pakietami urlopowymi
        public async Task<List<Employee>> GetAllEmployeesWithVacationsAsync()
        {
            return await _context.Employees
                .Include(e => e.Vacations)
                .Include(e => e.VacationPackage)
                .ToListAsync();
        }

        //  Pobranie pracownika po ID z urlopami i pakietem urlopowym
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Vacations)
                .Include(e => e.VacationPackage)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
