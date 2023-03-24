using AutoMapper;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Persistance.Repository
{
    public class EmployeeRepository
    {
        private readonly CarpoolContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(IMapper mapper, CarpoolContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            var dbEmployees = _context.Employee.AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<Employee>>(dbEmployees);
        }

        public IEnumerable<Employee> GetByIds(IEnumerable<int> ids)
        { 
            var dbEmployees = _context.Employee.AsNoTracking().Where(employee =>  ids.Contains(employee.Id));
            return _mapper.Map<IEnumerable<Employee>>(dbEmployees);
        }
    }
}