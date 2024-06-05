using PillPal.Application.Features.Nations;
using System.Text.Json.Serialization;

namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record PharmaceuticalCompanyDto
{
    public Guid Id { get; init; }
    public string? CompanyName { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NationDto Nation { get; init; } = default!;
}
