using Microsoft.AspNetCore.Mvc;
using VacationManagementSystem.Services;

namespace VacationManagementSystem.Controllers
{
    public class VacationManagmentController : Controller
    {
        private readonly VacationManagmentService _service;

        public VacationManagmentController(VacationManagmentService service)
        {
            _service = service;
        }

        //  Widok listy pracowników i statusów urlopowych
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _service.GetEmployeesVacationSummaryAsync();
            return View(employees);
        }

        //  Widok szczegółowy jednego pracownika
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _service.GetEmployeeDetailsAsync(id);
            if (employee == null)
                return NotFound($"Nie znaleziono pracownika o ID {id}.");

            return View(employee);
        }
    }
}
