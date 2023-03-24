using AutoMapper;
using CarpoolManagement.Models;
using CarpoolManagement.Source;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/rideshare")]
    [Produces("application/json")]
    public class RideShareController : ControllerBase
    {
        private readonly RideShareService _rideShareService;
        private readonly IMapper _mapper;

        public RideShareController([FromServices] RideShareService rideShareService, IMapper mapper)
        {
            _rideShareService = rideShareService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all ride share records
        /// </summary>
        /// <returns>All cars</returns>
        /// <response code="200">Returns All Ride Share Records</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<RideShare> GetAllRideShares()
        {
            return _rideShareService.GetAll();
        }

        /// <summary>
        /// Creates a new ride share record
        /// </summary>
        /// <param name="newRideShareRequest">The Create Ride Share Request</param>
        /// <returns>A newly created ride share</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /car
        ///     {
        ///         "startLocation": "Presov",
        ///         "endLocation": "Kosice",
        ///         "startDate": "2009-08-15T13:40:30",
        ///         "endDate": "2009-08-15T13:45:30",
        ///         "carPlate": "AB 123-CD",
        ///         "employeeIds": [1,2,3],
        ///         "driverId:": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns a newly created ride share</response>
        /// <response code="400">
        ///     <para>Request breaks one of the validation rules:</para>
        ///     <para>Start Date must be earlier than End Date</para>
        ///     <para>At least one passanger needs to be present</para>
        ///     <para>At least one employee must be driver</para>
        ///     <para>Car Plate does not exist</para>
        ///     <para>Exceeded max occupancy for car</para>
        ///     <para>Requested car is booked for time frame of ride share</para>
        /// </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public RideShare CreateRideShare(CreateRideShareRequest newRideShareRequest)
        {
            var newRideShare = _mapper.Map<RideShare>(newRideShareRequest);
            return _rideShareService.CreateRideShare(newRideShare);
        }

        /// <summary>
        /// Updates existing ride share record
        /// </summary>
        /// <param name="updateRideShareRequest">The Update Ride Share Request</param>
        /// <param name="id">The Ride Share ID</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /car
        ///     {
        ///         "startLocation": "Presov",
        ///         "endLocation": "Kosice",
        ///         "startDate": "2009-08-15T13:40:30",
        ///         "endDate": "2009-08-15T13:45:30",
        ///         "carPlate": "AB 123-CD",
        ///         "employeeIds": [1,2,3],
        ///         "driverId:": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Update was successful</response>
        /// <response code="400">
        ///     <para>Request breaks one of the validation rules:</para>
        ///     <para>Start Date must be earlier than End Date</para>
        ///     <para>At least one passanger needs to be present</para>
        ///     <para>At least one employee must be driver</para>
        ///     <para>Car Plate does not exist</para>
        ///     <para>Exceeded max occupancy for car</para>
        ///     <para>Requested car is booked for time frame of ride share</para>
        /// </response>
        [HttpPut]
        [Route("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, UpdateRideShareRequest updateRideShareRequest)
        {
            var rideShareUpdate = _mapper.Map<RideShare>(updateRideShareRequest);
            _rideShareService.UpdateRideShare(rideShareUpdate);

            return Ok();
        }

        /// <summary>
        /// Retrieves a ride share record identified by ID
        /// </summary>
        /// <returns>A found Ride Share Record</returns>
        /// <response code="200">Returns Found Ride Share Record</response>
        /// <response code="404">No record with provided id was found</response>
        [HttpGet]
        [Route("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public RideShareFullDetails? GetById(int id)
        {
            return _rideShareService.GetById(id);
        }

        /// <summary>
        /// Generates Ride Share Report organized by Year, Month, Car Plate
        /// </summary>
        /// <returns>Ride Share Reports</returns>
        /// <response code="200">Ride Share Reports</response>
        [HttpGet]
        [Route("report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<RideShareReport> GetRideShareReport()
        {
            return _rideShareService.GenerateReport();
        }

        /// <summary>
        /// Deletes a ride share record identified by id
        /// </summary>
        /// <response code="204">Record deleted</response>
        [HttpDelete]
        [Route("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        public IActionResult Delete(int id)
        {
            _rideShareService.DeleteRideShare(id);

            return NoContent();
        }
    }
}
