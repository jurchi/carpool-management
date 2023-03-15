using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Persistance.Repository
{
    public class EmployeeRepository
    {
        private readonly Dictionary<int, Employee> _employees;

        private readonly List<Employee> _employeesList = new()
        {
            new Employee { Id = 1, Name = "Sebastiana Chaudhari", IsDriver = true },
            new Employee { Id = 2, Name = "Garbán De Santiago", IsDriver = true },
            new Employee { Id = 3, Name = "Verginia McCallum", IsDriver = true },
            new Employee { Id = 4, Name = "Joleen Storstrand", IsDriver = true },
            new Employee { Id = 5, Name = "Durga Robbins", IsDriver = true },
            new Employee { Id = 6, Name = "Aeliana Grant", IsDriver = false },
            new Employee { Id = 7, Name = "Hamo Kumar", IsDriver = false },
            new Employee { Id = 8, Name = "Oskar Arnaud", IsDriver = false },
            new Employee { Id = 9, Name = "Rolando Waller", IsDriver = false },
            new Employee { Id = 10, Name = "Adam Dreessen", IsDriver = false }
        };

        public EmployeeRepository()
        {
            _employees = _employeesList.ToDictionary(keySelector: employee => employee.Id, elementSelector: employee => employee);
        }

        public IEnumerable<Employee> GetAll() => _employees.Values.ToList();
        public IEnumerable<Employee> GetByIds(IEnumerable<int> ids) => _employees.Values.Where(employee => ids.Contains(employee.Id));
    }
}