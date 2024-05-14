using PillPal.Core.Dtos.DrugInformations.Commands;
using PillPal.Core.Dtos.DrugInformations.Queries;

namespace PillPal.Service.Applications.DrugInformations;

public interface IDrugInformationService
{
    Task<IEnumerable<GetDrugInformationQuery>> GetDrugInformationsAsync();
    Task CreateDrugInformationAsync(CreateDrugInformationCommand drugInformation);
    Task<IEnumerable<GetDrugInformationQuery>> GetDrugInformationByDrugIdAsync(Guid drugId);
}
