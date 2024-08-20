namespace PillPal.Application.Features.Customers;

public record CustomerMealTimeDto
{
    /// <example>07:00</example>
    public string? BreakfastTime { get; init; }

    /// <example>12:00</example>
    public string? LunchTime { get; init; }

    /// <example>16:00</example>
    public string? AfternoonTime { get; init; }

    /// <example>18:00</example>
    public string? DinnerTime { get; init; }

    /// <example>00:15</example>
    public string? MealTimeOffset { get; init; }
}
