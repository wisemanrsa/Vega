using AutoMapper;
using vega_be.DTOs;
using vega_be.Models;

namespace vega_be.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeDto>();
            CreateMap<CarModel, CarModelDto>();
        }
    }
}