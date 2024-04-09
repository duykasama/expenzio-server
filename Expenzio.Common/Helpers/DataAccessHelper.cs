using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace Expenzio.Common.Helpers;

public static class DataAccessHelper
{
    private static IConfiguration? _configuration;
    private static IConfiguration Configuration
    {
        get => _configuration ?? throw new ArgumentNullException(nameof(_configuration));
        set => _configuration = value;
    }

    public static void SetConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GetConnectionString(string connectionName)
    {
        return Configuration!.GetConnectionString(connectionName) ?? throw new ArgumentNullException(nameof(connectionName));
    }

    public static string GetDefaultConnectionString()
    {
        return GetConnectionString("DefaultConnection");
    }

    public static void EnsureMigration(string assemblyName, string? connectionString = null)
    {
        connectionString ??= GetDefaultConnectionString();
        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
            .LogToConsole()
            .Build();

        var scripts = upgrader.GetDiscoveredScripts();
        if (scripts.Any()) 
        {
            upgrader.PerformUpgrade();
        }
        else 
        {
            Console.WriteLine("No scripts to run.");
        }
    }
}
