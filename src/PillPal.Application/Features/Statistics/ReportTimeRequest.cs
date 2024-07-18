using System.Globalization;

namespace PillPal.Application.Features.Statistics;

public record ReportTimeRequest
{
    /// <example>2024-12-31</example>
    public DateTime? StartDate { get; set; } = default!;

    /// <example>2024-12-31</example>
    public DateTime? EndDate { get; set; } = default!;
}

public class ReportTimeRequestValidator : AbstractValidator<ReportTimeRequest>
{
    public ReportTimeRequestValidator()
    {
        const string dateFormat = "yyyy-MM-dd";

        RuleFor(x => x.StartDate)
            .Must(ValidDate).WithMessage("Invalid date format. Must be {dateFormat}");

        RuleFor(x => x.EndDate)
            .Must(ValidDate).WithMessage("Invalid date format. Must be {dateFormat}");

        static bool ValidDate(DateTime? date)
        {
            return date == null ||
            DateTime.TryParseExact(date.Value.ToString(dateFormat), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
