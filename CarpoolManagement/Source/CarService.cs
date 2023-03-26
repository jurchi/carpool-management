using AutoMapper;
using CarpoolManagement.Persistance;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Source
{
    public class CarService
    {
        private readonly CarpoolContext _context;
        private readonly IMapper _mapper;

        public CarService(CarpoolContext context, IMapper mapper)
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
