using PillPal.Application.Dtos.Nations;

namespace PillPal.Application.Dtos.PharmaceuticalCompanies;

public record PharmaceuticalCompanyDto
{
    public Guid Id { get; init; }
    public string? CompanyName { get; init; }

    public NationDto Nation { get; init; } = default!;
}
