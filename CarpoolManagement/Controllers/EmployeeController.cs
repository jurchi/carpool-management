using CarpoolManagement.Models;
using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Employee> Get([FromServices] EmployeeRepository repository)
        {
            return repository.GetAll();
        }

        [HttpPost]
        public ActionResult<IEnumerable<Employee>> FindEmployees([FromBody] FindEmployeesRequest request, [FromServices] EmployeeRepository repository)
        {
            var employees = repository.GetByIds(request.Ids);
            return employees == null ? NotFound() : Ok(employees);
        }
    }
}
