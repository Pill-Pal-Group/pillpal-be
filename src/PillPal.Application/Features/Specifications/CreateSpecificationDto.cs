namespace PillPal.Application.Features.Specifications;

public record CreateSpecificationDto
{
    /// <example>Box</example>
    public string? TypeName { get; init; }
}

public class CreateSpecificationValidator : AbstractValidator<CreateSpecificationDto>
{
    public CreateSpecificationValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty()
            .MaximumLength(50);
    }
}