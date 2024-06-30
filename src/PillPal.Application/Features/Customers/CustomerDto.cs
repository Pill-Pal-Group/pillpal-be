using PillPal.Application.Features.Accounts;

namespace PillPal.Application.Features.Customers;

public record CustomerDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>CUS6060-555555</example>
    public string? CustomerCode { get; init; }

    /// <example>2002-01-01</example>
    public DateTimeOffset? Dob { get; init; }

    /// <example>Q9, HCMC, Vietnam</example>
    public string? Address { get; init; }

    /// <example>07:00</example>
    public string? BreakfastTime { get; init; }

    /// <example>12:00</example>
    public string? LunchTime { get; init; }

    /// <example>16:00</example>
    public string? AfternoonTime { get; init; }

    /// <example>18:00</example>
    public string? DinnerTime { get; init; }

    /// <example>15</example>
    public string? MealTimeOffset { get; init; }

    /// <example>3</example>
    public int LockoutCount { get; init; }

    public AccountDto ApplicationUser { get; init; } = default!;
}
