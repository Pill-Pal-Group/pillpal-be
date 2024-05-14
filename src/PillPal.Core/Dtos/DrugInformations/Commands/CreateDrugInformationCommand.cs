namespace PillPal.Core.Dtos.DrugInformations.Commands;
public record CreateDrugInformationCommand(
    string Manufacturer,
    string RegisterCompany,
    string RegisterNumber,
    string RegisterDate,
    string RegisterPlace,
    string Composition,
    string StorageCondition,
    string Instruction,
    string Overdose,
    string Interaction,
    string Precaution,
    string Price,
    string Status,
    string Note,
    Guid DrugId);

public class CreateDrugInformationCommandValidator : AbstractValidator<CreateDrugInformationCommand>
{
    public CreateDrugInformationCommandValidator()
    {
        RuleFor(x => x.Manufacturer).NotEmpty();
        RuleFor(x => x.RegisterCompany).NotEmpty();
        RuleFor(x => x.RegisterNumber).NotEmpty();
        RuleFor(x => x.RegisterDate).NotEmpty();
        RuleFor(x => x.RegisterPlace).NotEmpty();
        RuleFor(x => x.Composition).NotEmpty();
        RuleFor(x => x.StorageCondition).NotEmpty();
        RuleFor(x => x.Instruction).NotEmpty();
        RuleFor(x => x.Overdose).NotEmpty();
        RuleFor(x => x.Interaction).NotEmpty();
        RuleFor(x => x.Precaution).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Note).NotEmpty();
        RuleFor(x => x.DrugId).NotEmpty();
    }
}

