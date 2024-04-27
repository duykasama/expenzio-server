using Asp.Versioning;
using Asp.Versioning.Conventions;
using AutoMapper;
using Expenzio.Api.Controllers.GraphQLApi;
using Expenzio.Api.Settings;
using Expenzio.Api.Swagger;
using Expenzio.Common.Helpers;
using Expenzio.Common.Interfaces;
using Expenzio.DAL.Data;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Expenzio.Api.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection ConfigureServices(this IServiceCollection services) {
        EnsureRequiredAssembliesLoaded();
        services.AddScoped<ExpenzioDbContext>();
        var assemblyTypes = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .ToList();
        var autoRegisterableTypes = assemblyTypes.Where(t => t.IsInterface && typeof(IAutoRegisterable).IsAssignableFrom(t) && t != typeof(IAutoRegisterable));
        var numb = autoRegisterableTypes.Count();
        foreach (var registerableType in autoRegisterableTypes)
        {
            var implementationType = assemblyTypes.FirstOrDefault(t => t.IsClass && !t.IsAbstract && registerableType.IsAssignableFrom(t));
            if (implementationType is null) continue;
            services.AddScoped(registerableType, implementationType);
        }

        var config = new MapperConfiguration(AutoMapperConfigurationHelper.Configure);
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration) {
        var corsSettings = configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ?? throw new ArgumentNullException(nameof(CorsSettings));
        services.AddCors(options => {
            foreach (var policy in corsSettings.Policies) {
                options.AddPolicy(policy.Name, builder => {
                    builder.WithOrigins(policy.AllowedOrigins)
                        .WithMethods(policy.AllowedMethods)
                        .WithHeaders(policy.AllowedHeaders);
                    if (policy.AllowCredentials)
                        builder.AllowCredentials();
                });
            }
        });
        return services;
    }

    public static IServiceCollection ConfigureGraphQL(this IServiceCollection services) {
        var graphQl = services.AddGraphQLServer()
            .AddQueryType<BaseQuery>();
        var assemblyTypes = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .ToList();
        foreach (var type in assemblyTypes) {
            if (Attribute.IsDefined(type, typeof(ExtendObjectTypeAttribute)))
                graphQl.AddTypeExtension(type);
        }
        return services;
    }

    public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services) {
        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => 
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });

        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc(options => 
            {
                options.Conventions.Add(new VersionByNamespaceConvention());
            })
            .AddApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    private static void EnsureRequiredAssembliesLoaded() {
        var assemblyNames = new[] {
            "Expenzio.DAL",
            "Expenzio.Service",
        };
        foreach (var assemblyName in assemblyNames) {
            AppDomain.CurrentDomain.Load(assemblyName);
        }
    }
}
