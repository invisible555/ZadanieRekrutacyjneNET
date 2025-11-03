
using NETRekrutacja.Entity;


namespace RecruitmentTask.Domain.Services
{
    public class EmployeeStructureService
    {
        private readonly List<EmployeeStructure> _structure = new();

        
        public List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
        {
            _structure.Clear();

            foreach (var employee in employees)
            {
                int level = 1;
                var superior = employee.Superior;

                while (superior != null)
                {
                    _structure.Add(new EmployeeStructure
                    {
                        EmployeeId = employee.Id,
                        SuperiorId = superior.Id,
                        Level = level
                    });

                    superior = superior.Superior;
                    level++;
                }
            }

            return _structure;
        }


        public int? GetSuperiorRowOfEmployee(int employeeId, int superiorId)
        {
            return _structure
                .FirstOrDefault(x => x.EmployeeId == employeeId && x.SuperiorId == superiorId)
                ?.Level;
        }
    }
}
