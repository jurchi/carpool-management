using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Car> Get([FromServices] CarRepository repository)
        {
            return repository.GetAll();
        }
    }
}
