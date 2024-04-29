using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.DAL.Interfaces;

public interface IUserRepository : IGenericRepository<ExpenzioUser>, IAutoRegisterable
{
}
