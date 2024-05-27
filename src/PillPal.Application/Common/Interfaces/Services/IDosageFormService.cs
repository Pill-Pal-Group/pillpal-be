using PillPal.Application.Dtos.DosageForms;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IDosageFormService
{
    Task<DosageFormDto> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto);
    Task DeleteDosageFormAsync(Guid dosageFormId);
    Task<DosageFormDto> GetDosageFormByIdAsync(Guid dosageFormId);
    Task<IEnumerable<DosageFormDto>> GetDosageFormsAsync();
    Task<DosageFormDto> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto);
}
