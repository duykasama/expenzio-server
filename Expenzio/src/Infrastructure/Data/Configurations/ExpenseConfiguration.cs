using Expenzio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenzio.Infrastructure.Data.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired();
    }
}
