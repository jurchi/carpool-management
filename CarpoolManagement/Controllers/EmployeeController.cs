using CarpoolManagement.Models;
using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/employee")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Retrieves all Employee records from repository
        /// </summary>
        /// <param name="repository">The Employee repository</param>
        /// <returns>All Found Employees</returns>
        /// <response code="200">Returns All Found Employees</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Employee> Get([FromServices] EmployeeRepository repository)
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Retrieves all passengers found by request parameters
        /// </summary>
        /// <param name="request">The request parameters</param>
        /// <param name="repository">The Employee repository</param>
        /// <returns>All All Found Employees</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /employee
        ///     {
        ///        "ids": [1,2,3]
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns All Found Employees</response>
        [HttpPost]
        public IEnumerable<Employee> FindEmployees([FromBody] FindEmployeesRequest request, [FromServices] EmployeeRepository repository)
        {
            return repository.GetByIds(request.Ids);
        }
    }
}
