namespace Rekrutacja.DTO
{
    public class EmployeeVacationViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GrantedDays { get; set; }
        public int UsedDays { get; set; }
        public int FreeDays { get; set; }
        public bool CanRequestVacation { get; set; }
    }

}
