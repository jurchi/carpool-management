using CarpoolManagement.Models;
using CarpoolManagement.Source;
using CarpoolManagement.Source.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideShareController : ControllerBase
    {
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
                CarPlate = newRideShareRequest.CarPlate!,
                EmployeeIds = newRideShareRequest.EmployeeIds!,
                StartLocation = newRideShareRequest.StartLocation!,
                EndLocation = newRideShareRequest.EndLocation!,
                StartDate = newRideShareRequest.StartDate,
                EndDate = newRideShareRequest.EndDate,
                DriverId = newRideShareRequest.DriverId
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
                CarPlate = updateRideShareRequest.CarPlate!,
                EmployeeIds = updateRideShareRequest.EmployeeIds!,
                StartLocation = updateRideShareRequest.StartLocation!,
                EndLocation = updateRideShareRequest.EndLocation!,
                StartDate = updateRideShareRequest.StartDate,
                EndDate = updateRideShareRequest.EndDate,
                DriverId = updateRideShareRequest.DriverId
            };

            _rideShareService.UpdateRideShare(newRideShare);

            return Ok();
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public ActionResult<RideShare> GetById(int id)
        {
            var rideShare = _rideShareService.GetById(id);
            return rideShare == null ? NotFound() : Ok(rideShare);
        }

        [HttpGet]
        [Route("report")]
        public IEnumerable<RideShareReport> GetRideShareReport()
        {
            return _rideShareService.GenerateReport();
        }


        [HttpDelete]
        [Route("id/{id:int}")]
        public void Delete(int id)
        {
            _rideShareService.DeleteRideShare(id);
        }
    }
}
