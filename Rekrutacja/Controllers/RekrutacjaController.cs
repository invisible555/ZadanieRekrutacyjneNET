using Microsoft.AspNetCore.Mvc;
using VacationManagementSystem.Services;

namespace VacationManagementSystem.Controllers
{
    public class RekrutacjaController : Controller
    {
        private readonly RekrutacjaService _service;

        public RekrutacjaController(RekrutacjaService service)
        {
            _service = service;
        }

        // Widok listy pracowników i statusów urlopowych
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _service.GetEmployeesVacationSummary();
            return View(employees);
        }

        // Widok szczegółowy jednego pracownika
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _service.GetEmployeeDetails(id);
            if (employee == null)
                return NotFound($"Nie znaleziono pracownika o ID {id}.");

            return View(employee);
        }
    }
}
