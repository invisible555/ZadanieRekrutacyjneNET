namespace NETRekrutacja.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? SuperiorId { get; set; }
        public Employee? Superior { get; set; }
    }
}
