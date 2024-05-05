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
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.HasOne(e => e.User).WithMany(u => u.ExpenseCategories).HasForeignKey(e => e.UserId);
        });
    }
}
