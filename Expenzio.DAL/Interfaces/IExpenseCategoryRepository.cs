using Expenzio.Common.Attributes;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace expenzio.DAL.Interfaces;

[AutoRegister(ServiceLifetime.Scoped)]
public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory>
{
}
