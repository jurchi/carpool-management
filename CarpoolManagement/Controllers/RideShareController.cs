using CarpoolManagement.Models;
using CarpoolManagement.Source;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RideShareController : ControllerBase
    {
        //TODO: ADD REQUEST FOR POST/PUT + AutoMapper
        //TODO: ADD SWAGGER SUPPORT

        private readonly RideShareService _rideShareService;

        public RideShareController([FromServices] RideShareService rideShareService)
        {
            _rideShareService = rideShareService;
        }

        [HttpGet]
        public IEnumerable<RideShare> GetAllRideShares()
        {
            return _rideShareService.GetAll();
        }

        [HttpPost]
        public RideShare CreateRideShare(CreateRideShareRequest newRideShareRequest)
        {
            RideShare newRideShare = new()
            {
                CarPlate = newRideShareRequest.CarPlate,
                EmployeeIds = newRideShareRequest.EmployeeIds,
                StartLocation = newRideShareRequest.StartLocation,
                EndLocation = newRideShareRequest.EndLocation,
                StartDate = DateTime.Parse(newRideShareRequest.StartDate, new CultureInfo("sk")),
                EndDate = DateTime.Parse(newRideShareRequest.EndDate, new CultureInfo("sk"))
            };
            
             return _rideShareService.CreateRideShare(newRideShare);
        }

        [HttpPut]
        [Route("id/{id:int}")]
        public IActionResult Update(int id, UpdateRideShareRequest updateRideShareRequest)
        {
            RideShare newRideShare = new()
            {
                Id = id,
                CarPlate = updateRideShareRequest.CarPlate,
                EmployeeIds = updateRideShareRequest.EmployeeIds,
                StartLocation = updateRideShareRequest.StartLocation,
                EndLocation = updateRideShareRequest.EndLocation,
                StartDate = DateTime.Parse(updateRideShareRequest.StartDate, new CultureInfo("sk")),
                EndDate = DateTime.Parse(updateRideShareRequest.EndDate, new CultureInfo("sk"))
            };

            _rideShareService.UpdateRideShare(newRideShare);

            return Ok();
        }

        [HttpDelete]
        [Route("id/{id:int}")]
        public void Delete(int id)
        {
            _rideShareService.DeleteRideShare(id);
        }
    }
}
