namespace PillPal.Core.Dtos.Drug.Queries;

public record GetDrugQuery(
    string DrugCode,
    string DrugName,
    string DrugDescription,
    string SideEffect,
    string Indication,
    string Contraindication,
    string Warning,
    string ImageUrl);
