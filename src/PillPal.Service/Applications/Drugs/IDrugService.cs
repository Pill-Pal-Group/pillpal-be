using PillPal.Core.Dtos.Drugs.Commands;
using PillPal.Core.Dtos.Drugs.Queries;

namespace PillPal.Service.Applications.Drugs;

public interface IDrugService
{
    Task<IEnumerable<GetDrugQuery>> GetDrugsAsync();
    Task CreateDrugAsync(CreateDrugCommand drug);
}
