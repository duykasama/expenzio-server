using AutoMapper;

namespace Expenzio.Common.Interfaces;

public interface IAutoMapperConfigurer
{
    void Configure(IMapperConfigurationExpression config);
}
