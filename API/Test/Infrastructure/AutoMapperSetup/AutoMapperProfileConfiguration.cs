using AutoMapper;
using Test.Entities;
using Test.Requests;

namespace Test.Infrastructure.AutoMapperSetup
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("TemperatureAPIMapping")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<TemperatureViewModel, TemperatureEntity>()
               .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
               .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp));
        }
    }
}