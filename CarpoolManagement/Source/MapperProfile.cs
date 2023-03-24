using AutoMapper;
using CarpoolManagement.Models;
using CarpoolManagement.Persistance.Models;
using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Source
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, Car>().ReverseMap();
            CreateMap<EmployeeEntity, Employee>().ReverseMap();

            CreateMap<RideShareEntity, RideShareFullDetails>()
                .ForMember(dest => dest.Employees, opts => opts.MapFrom(src => src.RideShareEmployee.Select(rse => new Employee { Id = rse.EmployeeId })))
                .ReverseMap();

            CreateMap<RideShareEntity, RideShare>()
                .ForMember(dest => dest.EmployeeIds, opts => opts.MapFrom(src => src.RideShareEmployee.Select(rse => rse.EmployeeId)))
                .ForMember(dest => dest.CarPlate, opts => opts.MapFrom(src => src.Car.Plate))
                .ReverseMap();

            CreateMap<UpdateRideShareRequest, RideShare>();

            CreateMap<CreateRideShareRequest, RideShare>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    }
}
