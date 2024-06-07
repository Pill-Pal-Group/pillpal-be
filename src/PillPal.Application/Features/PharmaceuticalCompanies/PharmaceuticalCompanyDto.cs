using PillPal.Application.Features.Nations;
using System.Text.Json.Serialization;

namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record PharmaceuticalCompanyDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Pfizer</example>
    public string? CompanyName { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NationDto Nation { get; init; } = default!;
}
