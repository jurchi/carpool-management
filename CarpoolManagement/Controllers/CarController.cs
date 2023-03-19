using CarpoolManagement.Models;
using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        /// <summary>
        /// Retrieves all Car records from repository
        /// </summary>
        /// <param name="repository">The Cars repository</param>
        /// <returns>All cars</returns>
        [HttpGet]
        public IEnumerable<Car> Get([FromServices] CarRepository repository)
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Finds car by available search criteria.
        /// </summary>
        /// <param name="request">Search request</param>
        /// <param name="repository">The Cars repository</param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult<Car> GetCarByPlate([FromBody]FindCarRequest request, [FromServices] CarRepository repository)
        {
            var car = repository.GetByPlate(request.Plate);
            return car == null ? NotFound() : Ok(car);
        }
    }
}
