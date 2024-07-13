using PillPal.Application.Features.Accounts;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void AccountProfile()
    {
        CreateMap<ApplicationUser, AccountDto>().ReverseMap();
    }
}
