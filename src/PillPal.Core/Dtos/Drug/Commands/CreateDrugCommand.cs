using FluentValidation;

namespace PillPal.Core.Dtos.Drug.Commands;

public record CreateDrugCommand(
    string DrugCode,
    string DrugName,
    string DrugDescription,
    string SideEffect,
    string Indication,
    string Contraindication,
    string Warning,
    string ImageUrl);

public class CreateDrugCommandValidator : AbstractValidator<CreateDrugCommand>
{
    public CreateDrugCommandValidator()
    {
        RuleFor(x => x.DrugCode).NotEmpty();
        RuleFor(x => x.DrugName).NotEmpty();
        RuleFor(x => x.DrugDescription).NotEmpty();
        RuleFor(x => x.SideEffect).NotEmpty();
        RuleFor(x => x.Indication).NotEmpty();
        RuleFor(x => x.Contraindication).NotEmpty();
        RuleFor(x => x.Warning).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}
