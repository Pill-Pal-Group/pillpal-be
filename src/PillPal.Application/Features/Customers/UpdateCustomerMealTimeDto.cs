namespace PillPal.Application.Features.Customers;

public record UpdateCustomerMealTimeDto
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

public class UpdateCustomerMealTimeValidator : AbstractValidator<UpdateCustomerMealTimeDto>
{
    public UpdateCustomerMealTimeValidator()
    {
        var timeFormatRegex = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9](:[0-5][0-9])?$");

        RuleFor(x => x.BreakfastTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .Must(time => TimeOnly.Parse(time!) >= TimeOnly.Parse("05:00")
                && TimeOnly.Parse(time!) <= TimeOnly.Parse("09:00"))
            .WithMessage("Breakfast time must be between 05:00 and 09:00.");

        RuleFor(x => x.LunchTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .Must(time => TimeOnly.Parse(time!) >= TimeOnly.Parse("10:00")
                && TimeOnly.Parse(time!) <= TimeOnly.Parse("14:00"))
            .WithMessage("Lunch time must be between 10:00 and 14:00.");

        RuleFor(x => x.AfternoonTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .Must(time => TimeOnly.Parse(time!) >= TimeOnly.Parse("15:00")
                && TimeOnly.Parse(time!) <= TimeOnly.Parse("17:00"))
            .WithMessage("Afternoon time must be between 15:00 and 17:00.");

        RuleFor(x => x.DinnerTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .Must(time => TimeOnly.Parse(time!) >= TimeOnly.Parse("18:00")
                && TimeOnly.Parse(time!) <= TimeOnly.Parse("21:30"))
            .WithMessage("Dinner time must be between 18:00 and 21:30.");

        RuleFor(x => x.MealTimeOffset)
            .NotEmpty()
            .Must(time => TimeSpan.Parse(time!) >= TimeSpan.FromMinutes(0)
                && TimeSpan.Parse(time!) <= TimeSpan.FromHours(1))
            .WithMessage("Meal time offset must be between 00:00 and 01:00.");
    }
}
