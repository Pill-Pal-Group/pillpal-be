using AutoMapper;
using PillPal.Core.Dtos.DrugInformations.Commands;
using PillPal.Core.Dtos.DrugInformations.Queries;
using PillPal.Core.Models;
using PillPal.Infrastructure.Repository;

namespace PillPal.Service.Applications.DrugInformations;

public class DrugInformationService : BaseService, IDrugInformationService
{
    public DrugInformationService(UnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public async Task CreateDrugInformationAsync(CreateDrugInformationCommand drugInformation)
    {
        var entity = _mapper.Map<DrugInformation>(drugInformation);
        await _unitOfWork.DrugInformation.Insert(entity);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GetDrugInformationQuery>> GetDrugInformationByDrugIdAsync(Guid drugId)
    {
        var entity = await _unitOfWork.DrugInformation
            .Get(filter: drug => drug.DrugId == drugId);
        return _mapper.Map<IEnumerable<GetDrugInformationQuery>>(entity.ToList());
    }

    public async Task<IEnumerable<GetDrugInformationQuery>> GetDrugInformationsAsync()
    {
        var list = await _unitOfWork.DrugInformation.Get();
        return _mapper.Map<IEnumerable<GetDrugInformationQuery>>(list);
    }
}
