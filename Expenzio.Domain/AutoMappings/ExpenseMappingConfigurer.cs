using AutoMapper;
using Expenzio.Domain.Entities;
using Expenzio.Common.Interfaces;
using Expenzio.Domain.Models.Requests.Expense;

namespace Expenzio.Domain.AutoMappings;

public class ExpenseMappingConfigurer : IAutoMapperConfigurer
{
    public void Configure(IMapperConfigurationExpression config)
    {
        config.CreateMap<CreateExpenseRequest, Expense>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
