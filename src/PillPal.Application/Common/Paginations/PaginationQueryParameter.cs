namespace PillPal.Application.Common.Paginations;

public record PaginationQueryParameter
{
    /// <example>1</example>
    public int Page { get; init; } = 1;

    /// <example>10</example>
    public int PageSize { get; init; } = 10;
}