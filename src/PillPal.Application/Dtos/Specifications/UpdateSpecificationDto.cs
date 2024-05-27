﻿namespace PillPal.Application.Dtos.Specifications;

public record UpdateSpecificationDto
{
    public string? TypeName { get; init; }
    public string? Detail { get; init; }
}

public class UpdateSpecificationValidator : AbstractValidator<UpdateSpecificationDto>
{
    public UpdateSpecificationValidator()
    {
        RuleFor(x => x.TypeName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Detail).NotEmpty().MaximumLength(100);
    }
}
