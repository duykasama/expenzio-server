using Expenzio.Common.Interfaces;
using Expenzio.DAL.Data;

namespace Expenzio.Api;

public static class ServiceCollectionExtensions {
		public static IServiceCollection ConfigureServices(this IServiceCollection services) {
				services.AddScoped<ExpenzioDbContext>();
				var assemblyTypes = AppDomain
						.CurrentDomain
						.GetAssemblies()
						.SelectMany(t => t.GetTypes());
				var autoResgisterableTypes = assemblyTypes.Where(t => t.IsInterface && t.IsAssignableTo(typeof(IAutoRegisterable)));
				foreach (var registerableType in autoResgisterableTypes)
				{
						var implementationType = assemblyTypes.FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(registerableType));
						if (implementationType is null) continue;
						services.AddScoped(registerableType, implementationType);
				}
				return services;
		}
}
