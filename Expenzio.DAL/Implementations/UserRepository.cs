using Expenzio.DAL.Data;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.DAL.Implementations;

public class UserRepository : GenericRepository<ExpenzioUser>, IUserRepository
{
    public UserRepository(ExpenzioDbContext context) : base(context)
    {
    }
}
