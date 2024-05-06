namespace Expenzio.Application.Expenses.Commands.CreateExpense;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
