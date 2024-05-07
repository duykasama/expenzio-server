using Expenzio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenzio.Infrastructure.Data.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(256);
        builder.Property(t => t.MonetaryUnit)
            .HasColumnName("monetary_unit")
            .IsRequired();
        builder.Property(t => t.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);
        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();  
        builder.Property(t => t.Created)
            .HasColumnName("created")
            .IsRequired();
        builder.Property(t => t.LastModified)
            .HasColumnName("last_modified")
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasColumnName("created_by");
        builder.Property(t => t.LastModifiedBy)
            .HasColumnName("last_modified_by");
        builder.HasOne(t => t.Category)
            .WithMany(t => t.Expenses)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
