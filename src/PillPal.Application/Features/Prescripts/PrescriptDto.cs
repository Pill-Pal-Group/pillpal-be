using PillPal.Application.Features.PrescriptDetails;

namespace PillPal.Application.Features.Prescripts;

public record PrescriptDto
{   
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>https://monke.com/prescript-image.jpg</example>
    public string? PrescriptImage { get; init; }

    /// <example>2024-06-19</example>
    public DateTimeOffset ReceptionDate { get; init; }

    /// <example>Dr. Doof</example>
    public string? DoctorName { get; init; }

    /// <example>General Hospital</example>
    public string? HospitalName { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid CustomerId { get; init; }

    public IEnumerable<PrescriptDetailDto> PrescriptDetails { get; init; } = default!;
}
