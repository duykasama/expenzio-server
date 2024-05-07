using Expenzio.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Expenzio.Application.ExpenseCategories.EventHandlers;

public class ExpenseCategoryCreatedEventHandler : INotificationHandler<ExpenseCategoryCreatedEvent>
{
    private readonly ILogger<ExpenseCategoryCreatedEventHandler> _logger;

    public ExpenseCategoryCreatedEventHandler(ILogger<ExpenseCategoryCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ExpenseCategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Expenzio Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
