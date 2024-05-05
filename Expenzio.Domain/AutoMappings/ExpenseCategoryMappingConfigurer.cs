using AutoMapper;
using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Responses;

namespace Expenzio.Domain.AutoMappings;

public class ExpenseCategoryMappingConfigurer : IAutoMapperConfigurer
{
    public void Configure(IMapperConfigurationExpression config)
    {
        config.CreateMap<CreateExpenseCategoryRequest, ExpenseCategory>();
        config.CreateMap<ExpenseCategory, CreatedExpenseCategoryResponse>();
    }
}
