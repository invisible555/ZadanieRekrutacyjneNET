using Rekrutacja.Models;

namespace Rekrutacja.DTO
{
    public class EmployeeVacationDaysDTO
    {
        public Employee Employee { get; set; } = new Employee();
        public int UsedVacationDays { get; set; }
    }
}
