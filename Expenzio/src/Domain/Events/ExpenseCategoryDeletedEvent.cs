namespace Expenzio.Domain.Events;

public class ExpenseCategoryDeletedEvent : BaseEvent
{
    public ExpenseCategoryDeletedEvent(ExpenseCategory item)
    {
        Item = item;
    }

    public ExpenseCategory Item { get; }
}
