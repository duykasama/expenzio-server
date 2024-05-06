using Expenzio.Domain.Entities;

namespace Expenzio.Application.Expenses.Queries.GetExpensesWithPagination;

public class ExpenseBriefDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Expense, ExpenseBriefDto>();
        }
    }
}
