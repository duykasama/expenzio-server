namespace Expenzio.Domain.Models.Requests;

public class CreateExpenseCategoryRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
