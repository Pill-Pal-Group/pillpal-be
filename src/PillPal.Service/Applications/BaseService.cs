using AutoMapper;
using PillPal.Infrastructure.Repository;

namespace PillPal.Service.Applications;

public class BaseService
{
    protected readonly UnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
