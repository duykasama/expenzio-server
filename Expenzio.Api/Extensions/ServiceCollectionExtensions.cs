using System.Text;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using AutoMapper;
using Expenzio.Api.Controllers.GraphQLApi;
using Expenzio.Api.Settings;
using Expenzio.Api.Swagger;
using Expenzio.Common.Helpers;
using Expenzio.Common.Interfaces;
using Expenzio.DAL.Data;
using Expenzio.Service.Implementation;
using Expenzio.Service.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Expenzio.Api.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions {

    /// <summary>
    /// Configure services for dependency injection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The services.</returns>
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

    /// <summary>
    /// Configure CORS.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration) {
        var corsSettings = configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ?? throw new ArgumentNullException(nameof(CorsSettings));
        services.AddCors(options => {
            foreach (var policy in corsSettings.Policies) {
                options.AddPolicy(
                    policy.Name, builder => {
                        builder.WithOrigins(policy.AllowedOrigins)
                            .WithMethods(policy.AllowedMethods)
                            .WithHeaders(policy.AllowedHeaders);
                        if (policy.AllowCredentials)
                            builder.AllowCredentials();
                    }
                );
            }
        });
        return services;
    }

    /// <summary>
    /// Configure GraphQL.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection ConfigureGraphQL(this IServiceCollection services) {
        var graphQl = services.AddGraphQLServer()
            .AddAuthorization()
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

    /// <summary>
    /// Add Swagger with api versioning.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The services.</returns>
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

    /// <summary>
    /// Configure settings from appsettings.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration) {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        ArgumentNullException.ThrowIfNull(jwtSettings);
        services.AddSingleton<JwtSettings>(jwtSettings);

        var localizationSettings = configuration.GetSection(nameof(LocalizationSettings)).Get<LocalizationSettings>();
        ArgumentNullException.ThrowIfNull(localizationSettings);
        services.AddSingleton<LocalizationSettings>(localizationSettings);

        return services;
    }

    /// <summary>
    /// Add JWT authentication.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration) {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ?? throw new ArgumentNullException(nameof(JwtSettings));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuerSigningKey = true,
                };
            });
        return services;
    }

    /// <summary>
    /// Add controllers with localization.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The services.</returns>
    public static IServiceCollection AddControllersWithLocalization(this IServiceCollection services)
    {
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services
            .AddControllers()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();
        return services;
    }

    private static void EnsureRequiredAssembliesLoaded() {
        var assemblyNames = new[] 
        {
            "Expenzio.DAL",
            "Expenzio.Service",
        };
        foreach (var assemblyName in assemblyNames)
        {
            AppDomain.CurrentDomain.Load(assemblyName);
        }
    }
}
