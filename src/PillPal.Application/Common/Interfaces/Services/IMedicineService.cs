using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IMedicineService
{
    Task<IEnumerable<MedicineDto>> GetMedicinesAsync(
        MedicineQueryParameter queryParameter, MedicineIncludeParameter includeParameter);
    Task<MedicineDto> GetMedicineByIdAsync(Guid medicineId);
    Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto createMedicineDto);
    Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto);
    Task DeleteMedicineAsync(Guid medicineId);
}
