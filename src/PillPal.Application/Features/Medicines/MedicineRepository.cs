using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public class MedicineRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IFileReader fileReader)
    : BaseRepository(context, mapper, serviceProvider), IMedicineService
{
    public async Task<int> CreateMedicinesFromExcelBatchAsync(List<CreateMedicineFromExcelDto> medicinesToInsert)
    {
        #region Query all necessary entities in bulk
        var existingMedicines = await Context.Medicines
            .Where(m => medicinesToInsert.Select(dto => dto.MedicineName).Contains(m.MedicineName))
            .ToListAsync();

        var existingNations = await Context.Nations
            .Where(n => medicinesToInsert.Select(dto => dto.Nation).Contains(n.NationName))
            .ToListAsync();

        var existingPharmaceuticalCompanies = await Context.PharmaceuticalCompanies
            .Where(pc => medicinesToInsert.Select(dto => dto.PharmaceuticalCompanies).Contains(pc.CompanyName))
            .ToListAsync();

        var existingBrands = await Context.Brands
            .Where(b => medicinesToInsert.Select(dto => dto.Brand).Contains(b.BrandName))
            .ToListAsync();

        var existingDosageForms = await Context.DosageForms
            .Where(df => medicinesToInsert.Select(dto => dto.DosageForms).Contains(df.FormName))
            .ToListAsync();

        var categoryNames = medicinesToInsert.SelectMany(dto => dto.Categories!).Distinct().ToList();
        var existingCategories = await Context.Categories
            .Where(c => categoryNames.Contains(c.CategoryName!))
            .ToListAsync();

        var activeIngredientNames = medicinesToInsert
            .SelectMany(dto => dto.ActiveIngredients!).Distinct().ToList();
        var existingActiveIngredients = await Context.ActiveIngredients
            .Where(ai => activeIngredientNames.Contains(ai.IngredientName!))
            .ToListAsync();

        var existingSpecifications = await Context.Specifications
            .Where(s => medicinesToInsert.Select(dto => dto.Specifications).Contains(s.TypeName))
            .ToListAsync();
        #endregion

        var newMedicines = new List<Medicine>();

        var newNations = new Dictionary<string, Nation>();
        var newPharmaceuticalCompanies = new Dictionary<string, PharmaceuticalCompany>();
        var newBrands = new Dictionary<string, Brand>();
        var newDosageForms = new Dictionary<string, DosageForm>();
        var newSpecifications = new Dictionary<string, Specification>();
        var newActiveIngredients = new Dictionary<string, ActiveIngredient>();//
        var newCategories = new Dictionary<string, Category>();//

        foreach (var dto in medicinesToInsert)
        {
            if (existingMedicines.Any(m => m.MedicineName!.Equals(dto.MedicineName)))
            {
                continue;
            }

            var medicine = Mapper.Map<Medicine>(dto);

            #region Get or create related entities
            var nation = existingNations.FirstOrDefault(n => n.NationName == dto.Nation)
                ?? newNations!.GetOrAdd(dto.Nation, new Nation { Id = Guid.NewGuid(), NationName = dto.Nation });

            var pharmaceuticalCompany = existingPharmaceuticalCompanies.FirstOrDefault(pc => pc.CompanyName == dto.PharmaceuticalCompanies)
                ?? newPharmaceuticalCompanies!.GetOrAdd(dto.PharmaceuticalCompanies, new PharmaceuticalCompany { Id = Guid.NewGuid(), CompanyName = dto.PharmaceuticalCompanies, Nation = nation });

            var brand = existingBrands.FirstOrDefault(b => b.BrandName == dto.Brand)
                ?? newBrands!.GetOrAdd(dto.Brand, new Brand { Id = Guid.NewGuid(), BrandName = dto.Brand, BrandLogo = dto.BrandLogo, BrandUrl = dto.BrandUrl });

            var dosageForm = existingDosageForms.FirstOrDefault(df => df.FormName == dto.DosageForms)
                ?? newDosageForms!.GetOrAdd(dto.DosageForms, new DosageForm { Id = Guid.NewGuid(), FormName = dto.DosageForms });

            var specification = existingSpecifications.FirstOrDefault(s => s.TypeName == dto.Specifications)
                ?? newSpecifications!.GetOrAdd(dto.Specifications, new Specification { Id = Guid.NewGuid(), TypeName = dto.Specifications });

            List<ActiveIngredient> activeIngredients = [];
            foreach (var activeIngredient in activeIngredientNames)
            {
                foreach (var ingredient in dto.ActiveIngredients!)
                {
                    if (ingredient == activeIngredient)
                    {
                        activeIngredients.Add(existingActiveIngredients.FirstOrDefault(ai => ai.IngredientName == ingredient)
                            ?? newActiveIngredients!.GetOrAdd(ingredient, new ActiveIngredient { Id = Guid.NewGuid(), IngredientName = ingredient }));
                    }
                }
            }

            List<Category> categories = [];
            foreach (var categoryName in categoryNames)
            {
                foreach (var category in dto.Categories!)
                {
                    if (category == categoryName)
                    {
                        categories.Add(existingCategories.FirstOrDefault(c => c.CategoryName == category)
                            ?? newCategories!.GetOrAdd(category, new Category { Id = Guid.NewGuid(), CategoryName = category }));
                    }
                }
            }
            #endregion

            #region  Assign related entities
            medicine.Categories = categories;
            medicine.PharmaceuticalCompanies = [ pharmaceuticalCompany ];
            medicine.DosageForms = [ dosageForm ];
            medicine.MedicineInBrands = [ new MedicineInBrand { Brand = brand, Price = dto.Price, MedicineUrl = dto.MedicineUrl } ];
            medicine.ActiveIngredients = activeIngredients;
            medicine.Specification = specification;
            #endregion

            newMedicines.Add(medicine);

            #region Add newly created entities to the context
            if (!existingNations.Contains(nation)) Context.Nations.Add(nation);
            if (!existingPharmaceuticalCompanies.Contains(pharmaceuticalCompany)) Context.PharmaceuticalCompanies.Add(pharmaceuticalCompany);
            if (!existingBrands.Contains(brand)) Context.Brands.Add(brand);
            if (!existingDosageForms.Contains(dosageForm)) Context.DosageForms.Add(dosageForm);
            if (!existingSpecifications.Contains(specification)) Context.Specifications.Add(specification);
            
            foreach (var activeIngredient in newActiveIngredients.Values)
            {
                if (!existingActiveIngredients.Contains(activeIngredient)) Context.ActiveIngredients.Add(activeIngredient);
            }

            foreach (var category in newCategories.Values)
            {
                if (!existingCategories.Contains(category)) Context.Categories.Add(category);
            }
            #endregion
        }

        // Add new medicines to the context
        await Context.Medicines.AddRangeAsync(newMedicines);

        // Save changes in bulk
        // Returns the number of affected rows
        return await Context.SaveChangesAsync();
    }

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

    public async Task<PaginationResponse<MedicineDto>> GetMedicinesAsync(
        MedicineQueryParameter queryParameter, MedicineIncludeParameter includeParameter)
    {
        await ValidateAsync(queryParameter);
        
        var medicines = await Context.Medicines
            .AsNoTracking()
            .Where(m => !m.IsDeleted)
            .Filter(queryParameter)
            .Include(includeParameter)
            .ToPaginationResponseAsync<Medicine, MedicineDto>(queryParameter, Mapper);

        return medicines;
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

    public async Task<FileExecutionResult> ImportMedicinesAsync(Stream file, 
        MedicineExcelProperties properties, ExcelPropertyDelimiters delimiter)
    {
        var dataTable = fileReader.ReadExcelFile(file);

        // represents the number of qualified rows in the excel file
        int exeCount = 0;

        var medicineToInsert = new List<CreateMedicineFromExcelDto>();

        foreach (DataRow row in dataTable.Rows)
        {
            // Skip if any cell in the row is empty or null
            if (row.ItemArray.Any(cell => cell == null
                || string.IsNullOrWhiteSpace(cell.ToString())))
            {
                continue;
            }

            var medicine = new CreateMedicineFromExcelDto
            {
                MedicineName = row[properties.MedicineName].ToString(),
                RequirePrescript = row[properties.RequirePrescript].ToString()!.Equals("Có"),
                Image = row[properties.Image].ToString(),
                Specifications = row[properties.Specifications].ToString(),
                DosageForms = row[properties.DosageForms].ToString(),
                PharmaceuticalCompanies = row[properties.PharmaceuticalCompanies].ToString(),
                Brand = row[properties.Brand].ToString(),
                BrandLogo = row[properties.BrandLogo].ToString(),
                BrandUrl = row[properties.BrandUrl].ToString(),
                Price = row[properties.Price].ToString(),
                MedicineUrl = row[properties.MedicineUrl].ToString(),
                Nation = row[properties.Nation].ToString(),
                RegistrationNumber = row[properties.RegistrationNumber].ToString(),

                Categories = row[properties.Categories].ToString()?
                    .Split(delimiter.CategoryDelimeter)
                    .Select(c => c.Trim()).ToList(),
                ActiveIngredients = row[properties.ActiveIngredients].ToString()?
                    .Split(delimiter.IngredientDelimeter)
                    .Select(ai => ai.Trim()).ToList(),
            };

            medicineToInsert.Add(medicine);
            exeCount++;
        }

        // actual insertion of rows by affected rows in the database
        var affectedRows = await CreateMedicinesFromExcelBatchAsync(medicineToInsert);

        return new FileExecutionResult
        {
            ExcelExecutionCount = exeCount,
            AffectedRows = affectedRows
        };
    }
}
