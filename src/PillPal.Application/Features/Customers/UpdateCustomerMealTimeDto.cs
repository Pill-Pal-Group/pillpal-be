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
        var timeFormatRegex = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9](:[0-5][0-9])?$", 
            RegexOptions.NonBacktracking, TimeSpan.FromMilliseconds(250));

        RuleFor(x => x.BreakfastTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .LessThan(x => x.LunchTime)
            .WithMessage("Breakfast time must be before lunch time.");

        RuleFor(x => x.LunchTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .GreaterThan(x => x.BreakfastTime)
            .WithMessage("Lunch time must be after breakfast time.")
            .LessThan(x => x.AfternoonTime)
            .WithMessage("Lunch time must be before afternoon time.");

        RuleFor(x => x.AfternoonTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .GreaterThan(x => x.LunchTime)
            .WithMessage("Afternoon time must be after lunch time.")
            .LessThan(x => x.DinnerTime)
            .WithMessage("Afternoon time must be before dinner time.");

        RuleFor(x => x.DinnerTime)
            .NotEmpty()
            .Matches(timeFormatRegex)
            .GreaterThan(x => x.AfternoonTime)
            .WithMessage("Dinner time must be after afternoon time.")
            .Must(time => TimeSpan.Parse(time!) <= TimeSpan.FromHours(24))
            .WithMessage("Dinner time must be before 23:59.");

        RuleFor(x => x.MealTimeOffset)
            .NotEmpty()
            .Must(time => TimeSpan.Parse(time!) >= TimeSpan.FromMinutes(0)
                && TimeSpan.Parse(time!) <= TimeSpan.FromHours(1))
            .WithMessage("Meal time offset must be between 00:00 and 01:00.");
    }
}
