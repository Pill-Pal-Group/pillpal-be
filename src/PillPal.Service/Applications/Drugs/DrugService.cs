using AutoMapper;
using PillPal.Core.Dtos.Drugs.Commands;
using PillPal.Core.Dtos.Drugs.Queries;
using PillPal.Core.Models;
using PillPal.Infrastructure.Repository;

namespace PillPal.Service.Applications.Drugs;

public class DrugService : BaseService, IDrugService
{
    public DrugService(UnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public async Task CreateDrugAsync(CreateDrugCommand drug)
    {
        var entity = _mapper.Map<Drug>(drug);
        await _unitOfWork.Drug.Insert(entity);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GetDrugQuery>> GetDrugsAsync()
    {
        var list = await _unitOfWork.Drug.Get(includeProperties: ["DrugInformations", "Ingredients"]);
        return _mapper.Map<IEnumerable<GetDrugQuery>>(list);
    }
}
