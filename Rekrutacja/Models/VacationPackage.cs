namespace Rekrutacja.Models
{
    public class VacationPackage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GrantedDays { get; set; }
        public int Year { get; set; }

        // 🔹 Relacja 1 -> *
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
