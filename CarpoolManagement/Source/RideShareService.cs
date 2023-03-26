using AutoMapper;
using CarpoolManagement.Persistance;
using CarpoolManagement.Persistance.Models;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Source
{
    public class RideShareService
    {
        private readonly CarService _carRepository;
        private readonly EmployeeService _employeeRepository;
        private readonly CarpoolContext _context;
        private readonly IMapper _mapper;

        public RideShareService(CarService carRepository, EmployeeService employeeRepository, CarpoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _carRepository = carRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<RideShare> GetAll()
        {
            var dbRideShares = _context.RideShare
                .Include(r => r.Car)
                .Include(r => r.RideShareEmployee)
                .ToList();

            return _mapper.Map<IEnumerable<RideShare>>(dbRideShares);
        }

        public RideShare CreateRideShare(RideShare requestedRideShare)
        {
            Car car = GetCar(requestedRideShare.CarPlate);

            ValidateRideShareTimeFrame(requestedRideShare.StartDate, requestedRideShare.EndDate);

            ValidateCarIsAvailable(car.Plate, requestedRideShare.StartDate);

            ValidateIsDriver(requestedRideShare.EmployeeIds);

            int passengerCount = requestedRideShare.EmployeeIds.Count();

            ValidatePassengersCount(passengerCount);
            ValidateOccupancy(car, passengerCount);

            RideShareEntity dbRideShare = new()
            {
                StartDate = requestedRideShare.StartDate,
                EndDate = requestedRideShare.EndDate,
                StartLocation = requestedRideShare.StartLocation,
                EndLocation = requestedRideShare.EndLocation,
                DriverId = requestedRideShare.DriverId,
                CarId = car.Id
            };

            foreach (int id in requestedRideShare.EmployeeIds)
            {
                RideShareEmployeeEntity rideShareEmployeeModel = new()
                {
                    EmployeeId = id
                };

                dbRideShare.RideShareEmployee.Add(rideShareEmployeeModel);
            }

            _context.RideShare.Add(dbRideShare);
            _context.SaveChanges();

            requestedRideShare.Id = dbRideShare.Id;

            return requestedRideShare;
        }

        public RideShare UpdateRideShare(RideShare rideShareUpdateRequest)
        {
            RideShareEntity dbRideShare = _context.RideShare.Where(rideShare => rideShare.Id == rideShareUpdateRequest.Id)
                                        .Include(r => r.RideShareEmployee)
                                        .FirstOrDefault()
                                        ?? throw new KeyNotFoundException($"Ride Share with ID: {rideShareUpdateRequest.Id} could not be found");

            Car car = GetCar(rideShareUpdateRequest.CarPlate);
            dbRideShare.CarId = car.Id;

            ValidateRideShareTimeFrame(rideShareUpdateRequest.StartDate, rideShareUpdateRequest.EndDate);

            ValidateCarIsAvailable(car.Plate, rideShareUpdateRequest.StartDate, rideShareUpdateRequest.Id);

            ValidateIsDriver(rideShareUpdateRequest.EmployeeIds);

            int passengerCount = rideShareUpdateRequest.EmployeeIds.Count();
            ValidatePassengersCount(passengerCount);
            ValidateOccupancy(car, passengerCount);

            dbRideShare.RideShareEmployee.Clear();

            foreach (int id in rideShareUpdateRequest.EmployeeIds)
            {
                RideShareEmployeeEntity rideShareEmployeeModel = new()
                {
                    EmployeeId = id
                };

                dbRideShare.RideShareEmployee.Add(rideShareEmployeeModel);
            }

            dbRideShare.StartDate = rideShareUpdateRequest.StartDate;
            dbRideShare.EndDate = rideShareUpdateRequest.EndDate;
            dbRideShare.StartLocation = rideShareUpdateRequest.StartLocation;
            dbRideShare.EndLocation = rideShareUpdateRequest.EndLocation;
            dbRideShare.DriverId = rideShareUpdateRequest.DriverId;

            _context.SaveChanges();

            return rideShareUpdateRequest;
        }

        public IEnumerable<RideShareReport> GenerateReport()
        {
            var rideShares = GetAll();

            var reports = rideShares.GroupBy(rideShare => new { rideShare.StartDate.Year, rideShare.StartDate.Month, rideShare.CarPlate })
                                       .Select(r => new RideShareReport
                                       {
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
                report.Car = cars.FirstOrDefault(car => string.Equals(car.Plate, report.Car!.Plate, StringComparison.OrdinalIgnoreCase)) ?? report.Car;

                IEnumerable<int> passengerIds = report.Passengers.Select(passenger => passenger.Id).ToList();
                report.Passengers = employees.Where(employee => passengerIds.Contains(employee.Id)).ToList();
            }

            return reports;
        }

        public void DeleteRideShare(int id)
        {
            var rideShare = _context.RideShare.Find(id);

            if (rideShare != null)
            {
                _context.RideShare.Remove(rideShare);
                _context.SaveChanges();
            }
        }

        public RideShareFullDetails? GetById(int id)
        {
            var dbRideShare = _context.RideShare.Where(rideShare => rideShare.Id == id)
                .Include(rideShare => rideShare.Car)
                .Include(r => r.RideShareEmployee)
                .FirstOrDefault()
                                                ?? throw new KeyNotFoundException($"Ride Share with ID: {id} could not be found");

            var rideShare = _mapper.Map<RideShareFullDetails>(dbRideShare);

            List<int> employeeIds = rideShare.Employees.Select(employee => employee.Id).ToList();
            rideShare.Employees = _employeeRepository.GetByIds(employeeIds);

            return rideShare;
        }

        /// <summary>
        /// Retrieves a Car record by plate, if exists
        /// </summary>
        /// <param name="carPlate">The Car Plate</param>
        /// <returns>Found Car</returns>
        /// <exception cref="KeyNotFoundException">The Car does not exist</exception>
        private Car GetCar(string carPlate)
        {
            var car = _carRepository.GetByPlate(carPlate)
                ?? throw new KeyNotFoundException($"Car With Plate: {carPlate} does not exist");

            return car;
        }

        /// <summary>
        /// Validates whether Ride Share Time Frame is correctly requested.
        /// </summary>
        /// <param name="StarDate">The Ride Share Start Date</param>
        /// <param name="EndDate">The Ride Share End Date</param>
        /// <exception cref="BadHttpRequestException">End Date is sooner than Start Date</exception>
        private static void ValidateRideShareTimeFrame(DateTime StarDate, DateTime EndDate)
        {
            if (DateTime.Compare(StarDate, EndDate) > 0)
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
        /// Validates if car has enough space for requested passenger count
        /// </summary>
        /// <param name="car">The Car</param>
        /// <param name="passengerCount">passenger count</param>
        /// <exception cref="BadHttpRequestException">There are too many passengers</exception>
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
        /// <param name="carPlate">The Car Plate</param>
        /// <param name="rideStarDate">Start of the Ride</param>
        /// <param name="excludeRideShareId">The Id of ride Share to be excluded from search</param>
        /// <exception cref="BadHttpRequestException">The car is unavailable for time frame</exception>
        private void ValidateCarIsAvailable(string carPlate, DateTime rideStarDate, int? excludeRideShareId = null)
        {
            var rideShares = _context.RideShare.Where(rideShare => rideShare.Car != null && rideShare.Car.Plate == carPlate);

            if (excludeRideShareId.HasValue)
            {
                rideShares = rideShares.Except(rideShares.Where(rideShare => rideShare.Id == excludeRideShareId));
            }

            if (rideShares?.Any(rs => DateTime.Compare(rs.StartDate, rideStarDate) <= 0
                                      && DateTime.Compare(rideStarDate, rs.EndDate) < 0) == true)
            {
                throw new BadHttpRequestException($"Requested car: {carPlate}, is booked for time frame of ride share.");
            }
        }
    }
}
