using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Medicines;

namespace PillPal.Application.Repositories;

public class MedicineRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IMedicineService
{
    public async Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateMedicineDto>>();

        var validationResult = await validator.ValidateAsync(createMedicineDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var medicine = Mapper.Map<Medicine>(createMedicineDto);

        await Context.Medicines.AddAsync(medicine);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task DeleteMedicineAsync(Guid medicineId)
    {
        var medicine = await Context.Medicines
            .Where(m => m.Id == medicineId && !m.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Medicine), medicineId);

        medicine.IsDeleted = true;

        await Context.SaveChangesAsync();
    }

    public async Task<MedicineDto> GetMedicineByIdAsync(Guid medicineId)
    {
        var medicine = await Context.Medicines
            .Where(m => m.Id == medicineId && !m.IsDeleted)
            .Include(m => m.Specification)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.ActiveIngredients)
            .Include(m => m.Brands)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Medicine), medicineId);

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task<IEnumerable<MedicineDto>> GetMedicinesAsync()
    {
        var medicines = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .Include(m => m.Specification)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.ActiveIngredients)
            .Include(m => m.Brands)
            .ToListAsync();

        return Mapper.Map<IEnumerable<MedicineDto>>(medicines);
    }

    public Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto)
    {
        throw new NotImplementedException();
    }
}
