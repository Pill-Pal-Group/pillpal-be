namespace PillPal.Application.Common.Paginations;

public record PaginationResponse<TDto>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int PageCount { get; init; }
    public IEnumerable<TDto> Data { get; init; } = default!;
}
