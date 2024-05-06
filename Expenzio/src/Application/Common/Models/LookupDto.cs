using Expenzio.Domain.Entities;

namespace Expenzio.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Expense, LookupDto>();
            CreateMap<ExpenseCategory, LookupDto>();
        }
    }
}
