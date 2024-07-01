namespace PillPal.Application.Features.PackageCategories;

public record PackageCategoryDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Premium 1 month</example>
    public string? PackageName { get; init; }

    /// <example>Package for premium feature in 1 month</example>
    public string? PackageDescription { get; init; }

    /// <example>30</example>
    public int PackageDuration { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }

    /// <example>2024-01-01T00:00:00.0000000+00:00</example>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <example>2024-01-01T00:00:00.0000000+00:00</example>
    public DateTimeOffset? UpdatedAt { get; init; }

    /// <example>Id of account execute create</example>
    public string? CreatedBy { get; init; }

    /// <example>Id of account execute update</example>
    public string? UpdatedBy { get; init; }

    /// <example>True</example>
    public bool IsDeleted { get; init; }

    /// <example>2024-01-01T00:00:00.0000000+00:00</example>
    public DateTimeOffset? DeletedAt { get; init; }
}
