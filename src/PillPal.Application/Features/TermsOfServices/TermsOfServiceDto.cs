namespace PillPal.Application.Features.TermsOfServices;

public record TermsOfServiceDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>TOS6060-555555</example>
    public string? TosCode { get; init; }

    /// <example>Security Policy</example>
    public string? Title { get; init; }

    /// <example>Content of the security policy.</example>
    public string? Content { get; init; }

    /// <example>2021-01-01T00:00:00.0000000+00:00</example>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <example>2021-01-01T00:00:00.0000000+00:00</example>
    public DateTimeOffset? UpdatedAt { get; init; }

    /// <example>Id of account execute create</example>
    public string? CreatedBy { get; init; }

    /// <example>Id of account execute update</example>
    public string? UpdatedBy { get; init; }
}
