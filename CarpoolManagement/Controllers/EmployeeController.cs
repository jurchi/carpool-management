using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Employee> Get([FromServices] EmployeeRepository repository)
        {
            return repository.GetAll();
        }
    }
}
