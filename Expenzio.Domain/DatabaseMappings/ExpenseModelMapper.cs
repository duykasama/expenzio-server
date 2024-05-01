using Expenzio.Domain.Entities;
using Expenzio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Domain.DatabaseMappings;

public class ExpenseModelMapper : IDatabaseModelMapper
{
    public void MapToDatabaseModel(ModelBuilder builder)
    {
        builder.Entity<Expense>(entity =>
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
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.HasOne(e => e.User)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
