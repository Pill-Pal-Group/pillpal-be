using PillPal.Core.Dtos.DrugInformations.Queries;
using PillPal.Core.Dtos.Ingredients.Queries;

namespace PillPal.Core.Dtos.Drugs.Queries;

public record GetDrugQuery(
    Guid Id,
    string DrugCode,
    string DrugName,
    string DrugDescription,
    string SideEffect,
    string Indication,
    string Contraindication,
    string Warning,
    string ImageUrl,
    ICollection<GetDrugInformationQuery> DrugInformations,
    ICollection<GetIngredientQuery> Ingredients);
