namespace Expenzio.Domain.Events;

public class ExpenseCategoryCreatedEvent : BaseEvent
{
    public ExpenseCategoryCreatedEvent(Expense item)
    {
        Item = item;
    }

    public Expense Item { get; }
}
