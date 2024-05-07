using Expenzio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenzio.Infrastructure.Data.Configurations;

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(256)
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
    }
}
