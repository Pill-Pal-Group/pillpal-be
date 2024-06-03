namespace PillPal.Application.Dtos.Specifications;

public record CreateSpecificationDto
{
    public string? TypeName { get; init; }
    public string? Detail { get; init; }
}

public class CreateSpecificationValidator : AbstractValidator<CreateSpecificationDto>
{
    public CreateSpecificationValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Detail)
            .MaximumLength(100);
    }
}