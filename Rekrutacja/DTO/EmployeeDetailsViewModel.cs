using VacationManagementSystem.Services;

namespace VacationManagementSystem.DTO
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GrantedDays { get; set; }
        public int UsedDays { get; set; }
        public int FreeDays { get; set; }
        public bool CanRequestVacation { get; set; }
        public List<VacationItem> Vacations { get; set; } = new();
    }
}
