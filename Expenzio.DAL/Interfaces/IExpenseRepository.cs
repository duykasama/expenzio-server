using Expenzio.Common.Attributes;
using Expenzio.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Expenzio.DAL.Interfaces;

[AutoRegister(ServiceLifetime.Scoped)]
public interface IExpenseRepository : IGenericRepository<Expense>
{
}
