﻿namespace PillPal.Application.Dtos.Nations;

public record UpdateNationDto
{
    public string? NationCode { get; init; }
    public string? NationName { get; init; }
}

public class UpdateNationValidator : AbstractValidator<UpdateNationDto>
{
    public UpdateNationValidator()
    {
        RuleFor(x => x.NationCode)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.NationName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
