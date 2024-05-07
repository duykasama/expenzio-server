using Expenzio.Application.Common.Interfaces;
using Expenzio.Application.Common.Mappings;
using Expenzio.Application.Common.Models;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.Expenses.Queries.GetExpensesWithPagination;

public record GetExpensesWithPaginationQuery : IRequest<PaginatedList<ExpenseBriefDto>>
{
    public Guid ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetexpensesWithPaginationQueryHandler : IRequestHandler<GetExpensesWithPaginationQuery, PaginatedList<ExpenseBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetexpensesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ExpenseBriefDto>> Handle(GetExpensesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.CreateSet<Expense, Guid>()
            .Where(x => x.Id == request.ListId)
            .OrderBy(x => x.Created)
            .ProjectTo<ExpenseBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
