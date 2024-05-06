namespace Expenzio.Domain.Events;

public class ExpenseDeletedEvent : BaseEvent
{
    public ExpenseDeletedEvent(Expense item)
    {
        Item = item;
    }

    public Expense Item { get; }
}
