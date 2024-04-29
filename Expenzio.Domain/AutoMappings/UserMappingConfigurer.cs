using AutoMapper;
using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Authentication;

namespace Expenzio.Domain.AutoMappings;

public class UserMappingConfigurer : IAutoMapperConfigurer
{
    public void Configure(IMapperConfigurationExpression config)
    {
        config.CreateMap<RegisterRequest, ExpenzioUser>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? "0000000000"));
    }
}
