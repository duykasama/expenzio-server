using Expenzio.Common.Helpers;
using Expenzio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.DAL.Data;

public class ExpenzioDbContext : DbContext
{
		public ExpenzioDbContext()
		{
		}

		public ExpenzioDbContext(DbContextOptions<ExpenzioDbContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
				base.OnConfiguring(optionsBuilder);
				if (optionsBuilder.IsConfigured) return;
				optionsBuilder.UseNpgsql(DataAccessHelper.GetDefaultConnectionString());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
				var databaseMappers = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(assembly => assembly.GetTypes())
						.Where(type => typeof(IDatabaseModelMapper).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
						.Select(Activator.CreateInstance)
						.Cast<IDatabaseModelMapper>()
						.ToList();

				foreach (var mapper in databaseMappers)
				{
						mapper.MapToDatabaseModel(modelBuilder);
				}
		}
}
