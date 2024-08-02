namespace PillPal.Application.Features.CustomerPackages;

public record CustomerPackageDto
{
    /// <example>30</example>
    public int Duration { get; init; }

    /// <example>2024-07-01T00:00:00+00:00</example>
    public DateTimeOffset StartDate { get; init; }

    /// <example>2024-07-31T00:00:00+00:00</example>
    public DateTimeOffset EndDate { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }

    /// <example>false</example>
    public bool IsExpired { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid CustomerId { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid PackageCategoryId { get; init; }
}
