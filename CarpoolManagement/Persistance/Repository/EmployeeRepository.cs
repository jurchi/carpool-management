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
            new Employee { Id = 10, Name = "Adam Dreessen", IsDriver = false },
            new Employee { Id = 11, Name = "Elyse Ray", IsDriver = true },
            new Employee { Id = 12, Name = "Arlo Tyler", IsDriver = true },
            new Employee { Id = 13, Name = "Helena Houston", IsDriver = true },
            new Employee { Id = 14, Name = "Sylas Vo", IsDriver = true },
            new Employee { Id = 15, Name = "Artemis Maxwell", IsDriver = true },
            new Employee { Id = 16, Name = "Eden Leblanc", IsDriver = false },
            new Employee { Id = 17, Name = "Novalee Dennis", IsDriver = false },
            new Employee { Id = 18, Name = "Joanna Mendez", IsDriver = false },
            new Employee { Id = 19, Name = "Arthur Poole", IsDriver = false },
            new Employee { Id = 20, Name = "Bonnie Flynn", IsDriver = false },
            new Employee { Id = 21, Name = "Kannon Boyer", IsDriver = true },
            new Employee { Id = 22, Name = "Chaya Ashley", IsDriver = true },
            new Employee { Id = 23, Name = "Kylen Woods", IsDriver = true },
            new Employee { Id = 24, Name = "Reese Dougherty", IsDriver = true },
            new Employee { Id = 25, Name = "Brett Sierra", IsDriver = true },
            new Employee { Id = 26, Name = "Kohen Hill", IsDriver = false },
            new Employee { Id = 27, Name = "Lillie Becker", IsDriver = false },
            new Employee { Id = 28, Name = "Andy Mata", IsDriver = false },
            new Employee { Id = 29, Name = "Zev Alvarez", IsDriver = false },
            new Employee { Id = 30, Name = "Raylan Lane", IsDriver = false }
        };

        public EmployeeRepository()
        {
            _employees = _employeesList.ToDictionary(keySelector: employee => employee.Id, elementSelector: employee => employee);
        }

        public IEnumerable<Employee> GetAll() => _employees.Values.ToList();
        public IEnumerable<Employee> GetByIds(IEnumerable<int> ids) => _employees.Values.Where(employee => ids.Contains(employee.Id));
    }
}