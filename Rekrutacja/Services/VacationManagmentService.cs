using Microsoft.AspNetCore.DataProtection.Repositories;
using VacationManagementSystem.DTO;
using VacationManagementSystem.Models;
using VacationManagementSystem.Repository;

namespace VacationManagementSystem.Services
{
    public class VacationManagmentService
    {

        private readonly VacationManagmentRepository _repository;
        public VacationManagmentService(VacationManagmentRepository repository)
        {
            _repository = repository;

        }

       

        //Zadanie 3
        public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations,
    VacationPackage vacationPackage)
        {
            var grantedDays = vacationPackage.GrantedDays;
            var usedDays = vacations
                .Where(v => v.EmployeeId == employee.Id && v.DateUntil < DateTime.Now)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            var freeDays = grantedDays - usedDays;
            return freeDays < 0 ? 0 : freeDays;
        }

        //Zadanie 4
        public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations,
VacationPackage vacationPackage)
        {
            return CountFreeDaysForEmployee(employee, vacations, vacationPackage) > 0;
        }


        // Dane do widoku Index
        public List<EmployeeVacationViewModel> GetEmployeesVacationSummary()
        {
            var employees = _repository.GetAllEmployeesWithVacations();

            return employees.Select(e => new EmployeeVacationViewModel
            {
                EmployeeId = e.Id,
                Name = e.Name,
                GrantedDays = e.VacationPackage?.GrantedDays ?? 0,
                UsedDays = e.Vacations
                    .Where(v => v.DateUntil < DateTime.Now)
                    .Sum(v => (v.DateUntil - v.DateSince).Days + 1),
                FreeDays = CountFreeDaysForEmployee(e, e.Vacations.ToList(), e.VacationPackage),
                CanRequestVacation = IfEmployeeCanRequestVacation(e, e.Vacations.ToList(), e.VacationPackage)
            }).ToList();
        }

        // Dane do widoku Details
        public EmployeeDetailsViewModel? GetEmployeeDetails(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return null;

            var usedDays = employee.Vacations
                .Where(v => v.DateUntil < DateTime.Now)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            var freeDays = CountFreeDaysForEmployee(employee, employee.Vacations.ToList(), employee.VacationPackage);

            return new EmployeeDetailsViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                GrantedDays = employee.VacationPackage?.GrantedDays ?? 0,
                UsedDays = usedDays,
                FreeDays = freeDays,
                CanRequestVacation = IfEmployeeCanRequestVacation(employee, employee.Vacations.ToList(), employee.VacationPackage),
                Vacations = employee.Vacations.Select(v => new VacationItem
                {
                    DateSince = v.DateSince,
                    DateUntil = v.DateUntil
                }).ToList()
            };
        }
    }

  

   




}
