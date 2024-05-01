using Expenzio.Domain.Entities;
using Expenzio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Domain.DatabaseMappings;

public class RefreshTokenModelMapper : IDatabaseModelMapper
{
    public void MapToDatabaseModel(ModelBuilder builder)
    {
        builder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("refresh_token");
            entity.HasKey(e => e.Token);
            entity.Property(e => e.Token)
                .HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
            entity.Property(e => e.ValidUntil)
                .HasColumnName("valid_until");
            entity.Property(e => e.UserId)
                .HasColumnName("user_id");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");
            entity.HasOne(e => e.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UserId);
        });
    }
}
