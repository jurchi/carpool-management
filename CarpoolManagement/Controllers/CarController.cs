using CarpoolManagement.Models;
using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/car")]
    [Produces("application/json")]
    public class CarController : ControllerBase
    {
        /// <summary>
        /// Retrieves all Car records from repository
        /// </summary>
        /// <param name="repository">The Cars repository</param>
        /// <returns>All cars</returns>
        /// <response code="200">Returns All Found Car</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Car> Get([FromServices] CarRepository repository)
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Finds car by available search criteria.
        /// </summary>
        /// <param name="request">Search request</param>
        /// <param name="repository">The Cars repository</param>
        /// <returns>A Car with plate</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /car
        ///     {
        ///        "plate": "AB 123-CD"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns Found Car</response>
        /// <response code="404">Car was not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCarByPlate([FromBody]FindCarRequest request, [FromServices] CarRepository repository)
        {
            var car = repository.GetByPlate(request.Plate);
            return car == null ? NotFound() : Ok(car);
        }
    }
}
