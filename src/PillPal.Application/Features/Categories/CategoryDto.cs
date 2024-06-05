namespace PillPal.Application.Features.Categories;

public record CategoryDto
{
    public Guid Id { get; init; }
    public string? CategoryCode { get; init; }
    public string? CategoryName { get; init; }
}
