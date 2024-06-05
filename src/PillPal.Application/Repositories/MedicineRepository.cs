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

        var brands = await Context.Brands
            .Where(b => createMedicineDto.Brands.Contains(b.Id))
            .ToListAsync();

        var pharmaceuticalCompanies = await Context.PharmaceuticalCompanies
            .Where(pc => createMedicineDto.PharmaceuticalCompanys.Contains(pc.Id))
            .ToListAsync();

        var dosageForms = await Context.DosageForms
            .Where(df => createMedicineDto.DosageForms.Contains(df.Id))
            .ToListAsync();

        var activeIngredients = await Context.ActiveIngredients
            .Where(ai => createMedicineDto.ActiveIngredients.Contains(ai.Id))
            .ToListAsync();

        if (brands.Count != createMedicineDto.Brands.Count())
        {
            throw new NotFoundException(
                nameof(Brand),
                createMedicineDto.Brands.Except(brands.Select(b => b.Id)).FirstOrDefault()
            );
        }

        if (pharmaceuticalCompanies.Count != createMedicineDto.PharmaceuticalCompanys.Count())
        {
            throw new NotFoundException(
                nameof(PharmaceuticalCompany),
                createMedicineDto.PharmaceuticalCompanys.Except(pharmaceuticalCompanies.Select(pc => pc.Id)).FirstOrDefault()
            );
        }

        if (dosageForms.Count != createMedicineDto.DosageForms.Count())
        {
            throw new NotFoundException(
                nameof(DosageForm),
                createMedicineDto.DosageForms.Except(dosageForms.Select(df => df.Id)).FirstOrDefault());
        }

        if (activeIngredients.Count != createMedicineDto.ActiveIngredients.Count())
        {
            throw new NotFoundException(
                nameof(ActiveIngredient),
                createMedicineDto.ActiveIngredients.Except(activeIngredients.Select(ai => ai.Id)).FirstOrDefault()
            );
        }

        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;

        medicine.DosageForms = dosageForms;

        medicine.ActiveIngredients = activeIngredients;

        medicine.Brands = brands;

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

    public async Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<UpdateMedicineDto>>();

        var validationResult = await validator.ValidateAsync(updateMedicineDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var medicine = await Context.Medicines
            .Where(m => m.Id == medicineId && !m.IsDeleted)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.ActiveIngredients)
            .Include(m => m.Brands)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Medicine), medicineId);

        Mapper.Map(updateMedicineDto, medicine);

        var brands = await Context.Brands
            .Where(b => updateMedicineDto.Brands.Contains(b.Id))
            .ToListAsync();

        var pharmaceuticalCompanies = await Context.PharmaceuticalCompanies
            .Where(pc => updateMedicineDto.PharmaceuticalCompanys.Contains(pc.Id))
            .ToListAsync();

        var dosageForms = await Context.DosageForms
            .Where(df => updateMedicineDto.DosageForms.Contains(df.Id))
            .ToListAsync();

        var activeIngredients = await Context.ActiveIngredients
            .Where(ai => updateMedicineDto.ActiveIngredients.Contains(ai.Id))
            .ToListAsync();

        if (brands.Count != updateMedicineDto.Brands.Count())
        {
            throw new NotFoundException(
                nameof(Brand), updateMedicineDto.Brands.Except(brands.Select(b => b.Id)).FirstOrDefault()
            );
        }

        if (pharmaceuticalCompanies.Count != updateMedicineDto.PharmaceuticalCompanys.Count())
        {
            throw new NotFoundException(
                nameof(PharmaceuticalCompany), updateMedicineDto.PharmaceuticalCompanys.Except(pharmaceuticalCompanies.Select(pc => pc.Id)).FirstOrDefault()
            );
        }

        if (dosageForms.Count != updateMedicineDto.DosageForms.Count())
        {
            throw new NotFoundException(
                nameof(DosageForm), updateMedicineDto.DosageForms.Except(dosageForms.Select(df => df.Id)).FirstOrDefault()
            );
        }

        if (activeIngredients.Count != updateMedicineDto.ActiveIngredients.Count())
        {
            throw new NotFoundException(
                nameof(ActiveIngredient), updateMedicineDto.ActiveIngredients.Except(activeIngredients.Select(ai => ai.Id)).FirstOrDefault()
            );
        }

        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;

        medicine.DosageForms = dosageForms;

        medicine.ActiveIngredients = activeIngredients;

        medicine.Brands = brands;

        // manually change entity state to modified so that interceptor detects changes
        // hence updating the updated at field
        // not sure if this is needed or not, so currently commented out
        //Context.Medicines.Update(medicine);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }
}
