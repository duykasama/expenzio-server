using Expenzio.Domain.Entities;
using Expenzio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Domain.DatabaseMappings;

public class UserModelMapper : IDatabaseModelMapper
{
    public void MapToDatabaseModel(ModelBuilder builder)
    {
        builder.Entity<ExpenzioUser>(e => 
        {
            e.ToTable("expenzio_user");
            e.HasKey(e => e.Id);
            e.Property(e => e.Id).HasColumnName("id");
            e.Property(e => e.Email).HasColumnName("email");
            e.Property(e => e.Password).HasColumnName("password");
            e.Property(e => e.FirstName).HasColumnName("first_name");
            e.Property(e => e.LastName).HasColumnName("last_name");
            e.Property(e => e.CreatedAt).HasColumnName("created_at");
            e.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            e.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        });
    }
}
