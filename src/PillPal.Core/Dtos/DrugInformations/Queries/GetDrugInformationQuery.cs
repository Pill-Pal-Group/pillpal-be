namespace PillPal.Core.Dtos.DrugInformations.Queries;

public record GetDrugInformationQuery(
    Guid Id,
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
    string Note
);
