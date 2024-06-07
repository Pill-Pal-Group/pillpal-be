namespace PillPal.Application.Features.Specifications;

public record UpdateSpecificationDto
{
    /// <example>Box</example>
    public string? TypeName { get; init; }

    /// <example>2 per tablet</example>
    public string? Detail { get; init; }
}

public class UpdateSpecificationValidator : AbstractValidator<UpdateSpecificationDto>
{
    public UpdateSpecificationValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Detail)
            .MaximumLength(500);
    }
}
