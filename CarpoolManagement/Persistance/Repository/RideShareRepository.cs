using AutoMapper;
using CarpoolManagement.Persistance.Models;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Persistance.Repository
{
    public class RideShareRepository
    {
        private readonly CarpoolContext _context;
        private readonly IMapper _mapper;

        public RideShareRepository(CarpoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<RideShare> GetAll()
        {
            var dbRideShares = _context.RideShare
                .Include(r => r.Car)
                .Include(r => r.RideShareEmployee)
                .ToList();

            return _mapper.Map<IEnumerable<RideShare>>(dbRideShares);
        }

        public RideShareFullDetails? GetById(int id)
        {
            var dbRideShare = _context.RideShare.Where(rideShare => rideShare.Id == id)
                                                .Include(rideShare => rideShare.Car)
                                                .Include(r => r.RideShareEmployee)
                                                .FirstOrDefault();

            return _mapper.Map<RideShareFullDetails>(dbRideShare);
        }

        public RideShare Add(RideShare rideShare)
        {
            RideShareEntity dbRideShare = new()
            {
                StartDate = rideShare.StartDate,
                EndDate = rideShare.EndDate,
                StartLocation = rideShare.StartLocation,
                EndLocation = rideShare.EndLocation,
                DriverId = rideShare.DriverId,
            };

            dbRideShare.CarId = 1;

            foreach (int id in rideShare.EmployeeIds)
            {
                RideShareEmployeeEntity rideShareEmployeeModel = new()
                {
                    EmployeeId = id
                };

                dbRideShare.RideShareEmployee.Add(rideShareEmployeeModel);
            }

            _context.RideShare.Add(dbRideShare);            
            _context.SaveChanges();

            rideShare.Id = dbRideShare.Id;

            return rideShare;
        }

        public void Update(RideShare rideShare)
        {
            RideShareEntity? dbRideShare = _context.RideShare.Find(rideShare.Id);

            var currentEmployees = _context.RideShareEmployee.Where(rse => rse.RideShareId == rideShare.Id);
            _context.RideShareEmployee.RemoveRange(currentEmployees);
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

        public IEnumerable<RideShare> GetRideSharesForCar(string plate)
        {
            var rideShares = _context.RideShare.Where(rideShare => rideShare.Car != null && rideShare.Car.Plate == plate).ToList();
            return _mapper.Map<IEnumerable<RideShare>>(rideShares);
        }
    }
}
