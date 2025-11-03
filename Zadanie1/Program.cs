using NETRekrutacja.Entity;
using RecruitmentTask.Domain.Services;
namespace NETRekrutacja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var jan = new Employee { Id = 1, Name = "Jan Kowalski" };
            var kamil = new Employee { Id = 2, Name = "Kamil Nowak", Superior = jan };
            var andrzej = new Employee { Id = 4, Name = "Andrzej Abacki", Superior = kamil };
            var anna = new Employee { Id = 3, Name = "Anna Mariacka", Superior = jan };

            var employees = new List<Employee> { jan, kamil, anna, andrzej };

            var service = new EmployeeStructureService();
            service.FillEmployeesStructure(employees);

            Console.WriteLine("row 1: " + service.GetSuperiorRowOfEmployee(2, 1));
            Console.WriteLine("row 2: " + service.GetSuperiorRowOfEmployee(3, 2));
            Console.WriteLine("row 3: " + service.GetSuperiorRowOfEmployee(3, 1));
            Console.WriteLine("row 4: " + service.GetSuperiorRowOfEmployee(1, 2));
            Console.WriteLine("row 5: " + service.GetSuperiorRowOfEmployee(4, 1));
        }
    }
}