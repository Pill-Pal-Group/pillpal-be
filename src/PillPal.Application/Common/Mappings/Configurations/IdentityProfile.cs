using PillPal.Application.Dtos.Identities;
using PillPal.Core.Identity;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void IdentityProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
    }
}
