using AutoMapper;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Persistance.Repository
{
    public class CarRepository
    {
        private readonly CarpoolContext _context;
        private readonly IMapper _mapper;

        public CarRepository(CarpoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Car> GetAll() 
        { 
            var dbCars = _context.Car.AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<Car>>(dbCars);
        }

        public Car? GetByPlate(string plate)
        {
            var dbCar = _context.Car.AsNoTracking().FirstOrDefault(car => car.Plate == plate);
            return _mapper.Map<Car>(dbCar);
        }
    }
}
