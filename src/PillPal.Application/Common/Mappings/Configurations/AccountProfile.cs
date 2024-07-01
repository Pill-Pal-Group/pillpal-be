using PillPal.Application.Features.Accounts;
using PillPal.Core.Identity;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void AccountProfile()
    {
        CreateMap<ApplicationUser, AccountDto>().ReverseMap();
    }
}
