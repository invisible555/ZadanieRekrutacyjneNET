using NUnit.Framework;
using NUnit.Framework.Legacy;
using Rekrutacja.Models;
using Rekrutacja.Services;

namespace Rekrutacja.Tests.Repository
{
    [TestFixture]
    public class RekrutacjaServiceTests
    {
        private RekrutacjaService _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new RekrutacjaService();
        }

        [Test]
        public void employee_can_request_vacation()
        {
            // Arrange
            var employee = new Employee { Id = 1 };
            var vacationPackage = new VacationPackage { GrantedDays = 26, Year = DateTime.Now.Year };
            var vacations = new List<Vacation>
            {
                new Vacation { EmployeeId = 1, DateSince = DateTime.Now.AddDays(-10), DateUntil = DateTime.Now.AddDays(-5) }
            };

            // Act
            var result = _repo.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);
        

            // Assert
      
            Assert.That(result, Is.True, "Employee should be able to request vacation.");
            
        }

        [Test]
        public void employee_cant_request_vacation()
        {
            // Arrange
            var employee = new Employee { Id = 2 };
            var vacationPackage = new VacationPackage { GrantedDays = 5, Year = DateTime.Now.Year };
            var vacations = new List<Vacation>
            {
                new Vacation { EmployeeId = 2, DateSince = DateTime.Now.AddDays(-15), DateUntil = DateTime.Now.AddDays(-10) },
                new Vacation { EmployeeId = 2, DateSince = DateTime.Now.AddDays(-9), DateUntil = DateTime.Now.AddDays(-5) }
            };

            // Act
            var result = _repo.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);
            

            // Assert
            Assert.That(result, Is.False, "Employee should NOT be able to request vacation.");
    
        }
    }
}
