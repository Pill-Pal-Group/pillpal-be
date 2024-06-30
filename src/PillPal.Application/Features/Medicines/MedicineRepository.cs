using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;
using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public class MedicineRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IMedicineService
{
    public async Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
    {
        await ValidateAsync(createMedicineDto);

        var medicine = Mapper.Map<Medicine>(createMedicineDto);

        // get related entities from list of ids
        var categories = await GetEntitiesByIdsAsync(createMedicineDto.Categories, Context.Categories);
        var pharmaceuticalCompanies = await GetEntitiesByIdsAsync(createMedicineDto.PharmaceuticalCompanies, Context.PharmaceuticalCompanies);
        var dosageForms = await GetEntitiesByIdsAsync(createMedicineDto.DosageForms, Context.DosageForms);
        var activeIngredients = await GetEntitiesByIdsAsync(createMedicineDto.ActiveIngredients, Context.ActiveIngredients);

        // assign related entities
        medicine.Categories = categories;
        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;
        medicine.DosageForms = dosageForms;
        medicine.ActiveIngredients = activeIngredients;

        await Context.Medicines.AddAsync(medicine);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task CreateMedicineInBrandAsync(Guid medicineId, CreateMedicineInBrandsDto createMedicineInBrandDto)
    {
        await ValidateAsync(createMedicineInBrandDto);

        var medicineInBrand = Mapper.Map<MedicineInBrand>(createMedicineInBrandDto);

        var medicine = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .FirstOrDefaultAsync(m => m.Id == medicineId) 
            ?? throw new NotFoundException(nameof(Medicine), medicineId);

        medicineInBrand.Medicine = medicine;

        await Context.MedicineInBrands.AddAsync(medicineInBrand);

        await Context.SaveChangesAsync();
    }

    public async Task UpdateMedicineInBrandAsync(Guid medicineId, UpdateMedicineInBrandsDto updateMedicineInBrandDto)
    {
        await ValidateAsync(updateMedicineInBrandDto);

        var medicineInBrand = await Context.MedicineInBrands
            .FirstOrDefaultAsync(mib => mib.MedicineId == medicineId && mib.BrandId == updateMedicineInBrandDto.BrandId) 
            ?? throw new NotFoundException(nameof(MedicineInBrand), medicineId);

        Mapper.Map(updateMedicineInBrandDto, medicineInBrand);

        Context.MedicineInBrands.Update(medicineInBrand);

        await Context.SaveChangesAsync();
    }

    public async Task DeleteMedicineAsync(Guid medicineId)
    {
        var medicine = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .FirstOrDefaultAsync(m => m.Id == medicineId) 
            ?? throw new NotFoundException(nameof(Medicine), medicineId);

        Context.Medicines.Remove(medicine);

        await Context.SaveChangesAsync();
    }

    public async Task<MedicineDto> GetMedicineByIdAsync(Guid medicineId)
    {
        var medicine = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .Include(m => m.Specification)
            .Include(m => m.Categories)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.ActiveIngredients)
            .Include(m => m.MedicineInBrands.Where(mib => !mib.IsDeleted))
            .ThenInclude(mib => mib.Brand)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == medicineId) 
            ?? throw new NotFoundException(nameof(Medicine), medicineId);

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task<IEnumerable<MedicineDto>> GetMedicinesAsync(
        MedicineQueryParameter queryParameter, MedicineIncludeParameter includeParameter)
    {
        var medicines = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .Filter(queryParameter)
            .Include(includeParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<MedicineDto>>(medicines);
    }

    public async Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto)
    {
        await ValidateAsync(updateMedicineDto);

        var medicine = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .Include(m => m.Categories)
            .Include(m => m.Specification)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.ActiveIngredients)
            .FirstOrDefaultAsync(m => m.Id == medicineId) 
            ?? throw new NotFoundException(nameof(Medicine), medicineId);

        Mapper.Map(updateMedicineDto, medicine);

        // get related entities from list of ids
        var categories = await GetEntitiesByIdsAsync(updateMedicineDto.Categories, Context.Categories);
        var pharmaceuticalCompanies = await GetEntitiesByIdsAsync(updateMedicineDto.PharmaceuticalCompanies, Context.PharmaceuticalCompanies);
        var dosageForms = await GetEntitiesByIdsAsync(updateMedicineDto.DosageForms, Context.DosageForms);
        var activeIngredients = await GetEntitiesByIdsAsync(updateMedicineDto.ActiveIngredients, Context.ActiveIngredients);

        // assign related entities
        medicine.Categories = categories;
        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;
        medicine.DosageForms = dosageForms;
        medicine.ActiveIngredients = activeIngredients;

        // manually change entity state to modified so that interceptor detects changes
        // hence updating the updated at field
        // not sure if this is needed or not, so currently commented out
        //Context.Medicines.Update(medicine);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task DeleteMedicineInBrandAsync(Guid medicineId, Guid brandId)
    {
        var medicineInBrand = await Context.MedicineInBrands
            .FirstOrDefaultAsync(mib => mib.MedicineId == medicineId && mib.BrandId == brandId) 
            ?? throw new NotFoundException(nameof(MedicineInBrand), medicineId);

        Context.MedicineInBrands.Remove(medicineInBrand);

        await Context.SaveChangesAsync();
    }
}
