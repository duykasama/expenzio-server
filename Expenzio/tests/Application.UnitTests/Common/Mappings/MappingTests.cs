using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Expenzio.Application.Common.Interfaces;
using Expenzio.Application.Common.Models;
using Expenzio.Application.Expenses.Queries.GetExpensesWithPagination;
// using Expenzio.Application.ExpenseCategories.Queries.GetExpenses;
using Expenzio.Domain.Entities;
using NUnit.Framework;

namespace Expenzio.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    // [TestCase(typeof(ExpenseCategory), typeof(TodoListDto))]
    // [TestCase(typeof(Expense), typeof(TodoItemDto))]
    [TestCase(typeof(ExpenseCategory), typeof(LookupDto))]
    [TestCase(typeof(Expense), typeof(LookupDto))]
    [TestCase(typeof(Expense), typeof(ExpenseBriefDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
