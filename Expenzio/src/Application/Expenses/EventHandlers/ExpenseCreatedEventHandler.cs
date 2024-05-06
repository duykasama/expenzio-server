using Expenzio.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Expenzio.Application.Expenses.EventHandlers;

public class ExpenseCreatedEventHandler : INotificationHandler<ExpenseCreatedEvent>
{
    private readonly ILogger<ExpenseCreatedEventHandler> _logger;

    public ExpenseCreatedEventHandler(ILogger<ExpenseCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ExpenseCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Expenzio Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
