using Expenzio.Domain.Entities;
using Expenzio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Domain.DatabaseMappings;

public class ExpenseCategoryModelMapper : IDatabaseModelMapper
{
		public void MapToDatabaseModel(ModelBuilder builder)
		{
				builder.Entity<ExpenseCategory>(entity =>
				{
						entity.ToTable("expense_category");
						entity.HasKey(e => e.Id);
						entity.Property(e => e.Id).HasColumnName("id");
						entity.Property(e => e.Description).HasColumnName("description");
						entity.Property(e => e.Name).HasColumnName("name");
				});
		}
}
