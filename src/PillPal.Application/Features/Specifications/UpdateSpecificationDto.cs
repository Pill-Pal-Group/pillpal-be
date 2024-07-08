namespace PillPal.Application.Features.Specifications;

public record UpdateSpecificationDto
{
    /// <example>Box</example>
    public string? TypeName { get; init; }
}

public class UpdateSpecificationValidator : AbstractValidator<UpdateSpecificationDto>
{
    public UpdateSpecificationValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
