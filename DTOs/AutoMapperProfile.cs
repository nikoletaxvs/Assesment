using Assesment.Models;
using AutoMapper;

namespace Assesment.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Border, BorderDto>();
            CreateMap<BorderDto, Border>();
            CreateMap<CountryDto, Country>();
        }
    }
}
