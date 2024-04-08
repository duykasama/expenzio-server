using Expenzio.Common.Helpers;
using Expenzio.Domain.Entities;
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
				Console.WriteLine("OnModelCreating");
				modelBuilder.Entity<Expense>(entity =>
				{
						entity.ToTable("expense");
						entity.HasKey(e => e.Id);
						entity.Property(e => e.Id).HasColumnName("id");
						entity.Property(e => e.Amount).HasColumnName("amount");
						entity.Property(e => e.MonetaryUnit).HasColumnName("monetary_unit");
						entity.Property(e => e.Description).HasColumnName("description");
						entity.Property(e => e.CreatedAt).HasColumnName("created_at");
						entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
						entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
						entity.Property(e => e.CategoryId).HasColumnName("category_id");
				});
				Console.WriteLine("OnModelCreating end");
		}
}
