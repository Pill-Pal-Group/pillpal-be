﻿namespace PillPal.Application.Features.Customers;

public record UpdateCustomerDto
{
    /// <example>2002-01-01</example>
    public DateTimeOffset? Dob { get; init; }

    /// <example>Q9, HCMC, Vietnam</example>
    public string? Address { get; init; }
}

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Dob)
            .LessThan(DateTimeOffset.Now)
            .WithMessage("Dob must be less than current date.");

        RuleFor(x => x.Address)
            .MaximumLength(500);
    }
}
