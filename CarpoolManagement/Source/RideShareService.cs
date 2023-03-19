using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Source
{
    public class RideShareService
    {
        private readonly RideShareRepository _rideShareRepository;
        private readonly CarRepository _carRepository;
        private readonly EmployeeRepository _employeeRepository;

        public RideShareService(RideShareRepository rideShareRepository, CarRepository carRepository, EmployeeRepository employeeRepository)
        {
            _rideShareRepository = rideShareRepository;
            _carRepository = carRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<RideShare> GetAll() => _rideShareRepository.GetAll();

        public RideShare CreateRideShare(RideShare requestedRideShare)
        {
            RideShareBusinessValidation(requestedRideShare);

            return _rideShareRepository.Add(requestedRideShare);
        }

        public void UpdateRideShare(RideShare requestedRideShare)
        {
            RideShareBusinessValidation(requestedRideShare);

            _rideShareRepository.Update(requestedRideShare);
        }

        public IEnumerable<RideShareReport> GenerateReport()
        {
            var rideShares = _rideShareRepository.GetAll();

            var reports = rideShares.GroupBy(rideShare => new { rideShare.StartDate.Year, rideShare.StartDate.Month, rideShare.CarPlate })
                                       .Select(r => new RideShareReport {
                                           Year = r.Key.Year,
                                           Month = r.Key.Month,
                                           Car = new Car { Plate = r.Key.CarPlate },
                                           Passengers = r.SelectMany(r => r.EmployeeIds)
                                                         .Distinct()
                                                         .Select(id => new Employee { Id = id }),
                                           Trips = r.Count()
                                       }).ToList();

            var cars = _carRepository.GetAll();
            var employees = _employeeRepository.GetAll();

            foreach (var report in reports)
            {
                report.Car = cars.FirstOrDefault(car => String.Equals(car.Plate, report.Car.Plate, StringComparison.OrdinalIgnoreCase)) ?? report.Car;

                IEnumerable<int> passengerIds = report.Passengers.Select(passenger => passenger.Id).ToList();
                report.Passengers = employees.Where(employee => passengerIds.Contains(employee.Id)).ToList();
            }

            return reports;
        }

        public void DeleteRideShare(int id) => _rideShareRepository.DeleteRideShare(id);

        public RideShare? GetById(int id) => _rideShareRepository.GetById(id);

        /// <summary>
        /// Handles All Business Validation for Ride Share
        /// </summary>
        /// <param name="rideSharePlan"></param>
        private void RideShareBusinessValidation(RideShare rideSharePlan)
        {
            ValidateRideShareTimeFrame(rideSharePlan.StartDate, rideSharePlan.EndDate);

            int passengerCount = rideSharePlan.EmployeeIds.Count();

            ValidatePassengersCount(passengerCount);

            ValidateIsDriver(rideSharePlan.EmployeeIds);

            Car? car = _carRepository.GetByPlate(rideSharePlan.CarPlate);

            ValidateCarExists(car!);

            ValidateOccupancy(car!, passengerCount);

            ValidateCarIsAvailable(rideSharePlan.CarPlate, rideSharePlan.StartDate);
        }

        /// <summary>
        /// Validates whether Ride Share Time Frame is correctly requested.
        /// </summary>
        /// <param name="StarDate">The Ride Share Start Date</param>
        /// <param name="EndDate">The Ride Share End Date</param>
        /// <exception cref="BadHttpRequestException">End Date is sooner than Start Date</exception>
        private static void ValidateRideShareTimeFrame(DateTime StarDate, DateTime EndDate)
        {
            if (DateTime.Compare(StarDate, EndDate) >= 0)
            {
                throw new BadHttpRequestException("Start Date must be earlier than End Date");
            }
        }

        /// <summary>
        /// Validates if there is a rider requested for travel plan.
        /// </summary>
        /// <param name="ridersCount">Requested Riders Count</param>
        /// <exception cref="BadHttpRequestException">No riders requested</exception>
        private static void ValidatePassengersCount(int ridersCount)
        {
            if (ridersCount <= 0)
            {
                throw new BadHttpRequestException("At least one passanger needs to be present");
            }
        }

        /// <summary>
        /// Validate if any passenger has a dricing license.
        /// </summary>
        /// <param name="employeeIds">List of passenger Ids</param>
        /// <exception cref="BadHttpRequestException">No passenger can drive</exception>
        private void ValidateIsDriver(IEnumerable<int> employeeIds)
        {
            if (!_employeeRepository.GetByIds(employeeIds).Any(employee => employee.IsDriver))
            {
                throw new BadHttpRequestException($"At least one employee must be driver.");
            }
        }

        /// <summary>
        /// Validates if Car exists
        /// </summary>
        /// <param name="car">The Car</param>
        /// <exception cref="BadHttpRequestException">Car does not exist in DB</exception>
        private void ValidateCarExists(Car car)
        {
            if (car == null)
            {
                throw new BadHttpRequestException("Car Plate does not exist");
            }
        }

        /// <summary>
        /// Validates if car has enough space for requested passenger count
        /// </summary>
        /// <param name="car">The Car</param>
        /// <param name="passengerCount">passenger count</param>
        /// <exception cref="BadHttpRequestException"></exception>
        private static void ValidateOccupancy(Car car, int passengerCount)
        {
            if (car.NumberOfSeats < passengerCount)
            {
                throw new BadHttpRequestException($"Max occupancy for car:{car.Plate}, is {car.NumberOfSeats}. Requested {passengerCount}.");
            }
        }

        /// <summary>
        /// Validates if car is available for the time frame.
        /// </summary>
        /// <param name="carPlate"></param>
        /// <param name="rideStarDate"></param>
        /// <exception cref="BadHttpRequestException"></exception>
        private void ValidateCarIsAvailable(string carPlate, DateTime rideStarDate)
        {
            var carRideShares = _rideShareRepository.GetRideSharesForCar(carPlate);

            if (carRideShares?.Any(rs => DateTime.Compare(rs.StartDate, rideStarDate) <= 0
                                      && DateTime.Compare(rideStarDate, rs.EndDate) < 0) == true)
            {
                throw new BadHttpRequestException($"Requested car:{carPlate}, is booked for time frame of ride share.");
            }
        }
    }
}
