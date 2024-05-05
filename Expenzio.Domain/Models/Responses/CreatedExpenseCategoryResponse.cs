namespace Expenzio.Domain.Models.Responses;

public class CreatedExpenseCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
