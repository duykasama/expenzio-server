namespace Expenzio.Domain.Events;

public class ExpenseCreatedEvent : BaseEvent
{
    public ExpenseCreatedEvent(Expense item)
    {
        Item = item;
    }

    public Expense Item { get; }
}
