using PillPal.Application.Features.Nations;

namespace PillPal.Application.Common.Interfaces.Services;

public interface INationService
{
    Task<IEnumerable<NationDto>> GetNationsAsync(NationQueryParameter queryParameter);
    Task<NationDto> GetNationByIdAsync(Guid nationId);
    Task<NationDto> CreateNationAsync(CreateNationDto createNationDto);
    Task<NationDto> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto);
    Task DeleteNationAsync(Guid nationId);
}
