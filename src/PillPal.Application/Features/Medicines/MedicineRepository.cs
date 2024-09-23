using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Features.Medicines;

public class MedicineRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IFileReader fileReader)
    : BaseRepository(context, mapper, serviceProvider), IMedicineService
{
    private static (decimal price, string priceUnit) ParsePrice(string priceString)
    {
        var priceDelimiter = priceString.Contains('đ') ? "đ" : "₫";
        var notAvailablePrice = "N/A";

        var priceCombination = priceString.Split(priceDelimiter, 
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (priceCombination.Length != 2)
        {
            return (0, notAvailablePrice);
        }

        var priceValue = priceCombination[0].Replace(".", "");
        var priceUnit = priceCombination[1].Replace("/", "").Trim().ToLower();

        return (decimal.Parse(priceValue), priceUnit);
    }

    public async Task<int> CreateMedicinesFromExcelBatchAsync(List<CreateMedicineFromExcelDto> excelMedicineListToInsert)
    {
        #region Query all necessary entities in bulk
        var existingMedicinesList = await Context.Medicines
            .Include(m => m.MedicineInBrands)
            .ThenInclude(mib => mib.Brand)
            .Where(m => excelMedicineListToInsert.Select(dto => dto.RegistrationNumber).Contains(m.RegistrationNumber))
            .ToListAsync();

        var existingNationsList = await Context.Nations
            .Where(n => excelMedicineListToInsert.Select(dto => dto.Nation).Contains(n.NationName))
            .ToListAsync();

        var existingPharmaceuticalCompaniesList = await Context.PharmaceuticalCompanies
            .Where(pc => excelMedicineListToInsert.Select(dto => dto.PharmaceuticalCompanies).Contains(pc.CompanyName))
            .ToListAsync();

        var existingBrandsList = await Context.Brands
            .Where(b => excelMedicineListToInsert.Select(dto => dto.Brand).Contains(b.BrandName))
            .ToListAsync();

        var existingDosageFormsList = await Context.DosageForms
            .Where(df => excelMedicineListToInsert.Select(dto => dto.DosageForms).Contains(df.FormName))
            .ToListAsync();

        var existingSpecificationsList = await Context.Specifications
            .Where(s => excelMedicineListToInsert.Select(dto => dto.Specifications).Contains(s.TypeName))
            .ToListAsync();

        var categoryNames = excelMedicineListToInsert
            .SelectMany(dto => dto.Categories!).Distinct().ToList();
        var existingCategoriesList = await Context.Categories
            .Where(c => categoryNames.Contains(c.CategoryName!))
            .ToListAsync();

        var activeIngredientNames = excelMedicineListToInsert
            .SelectMany(dto => dto.ActiveIngredients!).Distinct().ToList();
        var existingActiveIngredientsList = await Context.ActiveIngredients
            .Where(ai => activeIngredientNames.Contains(ai.IngredientName!))
            .ToListAsync();
        #endregion

        var newMedicinesList = new List<Medicine>();

        var newNationsDict = new Dictionary<string, Nation>();
        var newPharmaceuticalCompaniesDict = new Dictionary<string, PharmaceuticalCompany>();
        var newBrandsDict = new Dictionary<string, Brand>();
        var newDosageFormsDict = new Dictionary<string, DosageForm>();
        var newSpecificationsDict = new Dictionary<string, Specification>();
        var newActiveIngredientsDict = new Dictionary<string, ActiveIngredient>();
        var newCategoriesDict = new Dictionary<string, Category>();

        foreach (var medicineRow in excelMedicineListToInsert)
        {
            // convert the price string to decimal and price unit
            (decimal price, string priceUnit) = ParsePrice(medicineRow.Price!);

            #region Handle medicine row that already exists in the database
            // check if the current medicine row already exists in the database by registration number
            if (existingMedicinesList.Any(m => m.RegistrationNumber!.Equals(medicineRow.RegistrationNumber)))
            {
                // if exists, check if the brand associated with the medicine row exists in the database
                // if the brand not exists (means new brand), then add the brand to the database
                var existingMedicine = existingMedicinesList
                    .FirstOrDefault(m => m.RegistrationNumber == medicineRow.RegistrationNumber);

                // get the brand associated with the medicine row
                var existingMedicineInBrand = existingMedicine!.MedicineInBrands
                    .FirstOrDefault(mib => mib.Brand!.BrandName == medicineRow.Brand);

                if (existingMedicineInBrand == null) // meaning new brand
                {
                    var newBrandRow = existingBrandsList.FirstOrDefault(b => b.BrandName == medicineRow.Brand)
                        ?? newBrandsDict!.GetOrAdd(medicineRow.Brand, new Brand { Id = Guid.NewGuid(), BrandName = medicineRow.Brand, BrandLogo = medicineRow.BrandLogo, BrandUrl = medicineRow.BrandUrl });

                    var medicineInBrand = new MedicineInBrand { Brand = newBrandRow, Price = price, PriceUnit = priceUnit, MedicineUrl = medicineRow.MedicineUrl };

                    existingMedicine.MedicineInBrands.Add(medicineInBrand);

                    // add the brand to the context
                    if (!existingBrandsList.Contains(newBrandRow)) Context.Brands.Add(newBrandRow);
                }
                else
                {
                    // incase the brand price has existed, update the price and price unit
                    if (existingMedicineInBrand.Price != price)
                    {
                        existingMedicineInBrand.Price = price;
                        existingMedicineInBrand.PriceUnit = priceUnit;
                        Context.MedicineInBrands.Update(existingMedicineInBrand);
                    }
                }

                continue;
            }
            #endregion

            var medicine = Mapper.Map<Medicine>(medicineRow);

            #region Get or create related entities
            var nation = existingNationsList.FirstOrDefault(n => n.NationName == medicineRow.Nation)
                ?? newNationsDict!.GetOrAdd(medicineRow.Nation, new Nation { Id = Guid.NewGuid(), NationName = medicineRow.Nation });

            var pharmaceuticalCompany = existingPharmaceuticalCompaniesList.FirstOrDefault(pc => pc.CompanyName == medicineRow.PharmaceuticalCompanies)
                ?? newPharmaceuticalCompaniesDict!.GetOrAdd(medicineRow.PharmaceuticalCompanies, new PharmaceuticalCompany { Id = Guid.NewGuid(), CompanyName = medicineRow.PharmaceuticalCompanies, Nation = nation });

            var brand = existingBrandsList.FirstOrDefault(b => b.BrandName == medicineRow.Brand)
                ?? newBrandsDict!.GetOrAdd(medicineRow.Brand, new Brand { Id = Guid.NewGuid(), BrandName = medicineRow.Brand, BrandLogo = medicineRow.BrandLogo, BrandUrl = medicineRow.BrandUrl });

            var dosageForm = existingDosageFormsList.FirstOrDefault(df => df.FormName == medicineRow.DosageForms)
                ?? newDosageFormsDict!.GetOrAdd(medicineRow.DosageForms, new DosageForm { Id = Guid.NewGuid(), FormName = medicineRow.DosageForms });

            var specification = existingSpecificationsList.FirstOrDefault(s => s.TypeName == medicineRow.Specifications)
                ?? newSpecificationsDict!.GetOrAdd(medicineRow.Specifications, new Specification { Id = Guid.NewGuid(), TypeName = medicineRow.Specifications });

            List<ActiveIngredient> activeIngredients = [];
            foreach (var activeIngredient in activeIngredientNames)
            {
                foreach (var ingredient in medicineRow.ActiveIngredients!)
                {
                    if (ingredient == activeIngredient)
                    {
                        activeIngredients.Add(existingActiveIngredientsList.FirstOrDefault(ai => ai.IngredientName == ingredient)
                            ?? newActiveIngredientsDict!.GetOrAdd(ingredient, new ActiveIngredient { Id = Guid.NewGuid(), IngredientName = ingredient }));
                    }
                }
            }

            List<Category> categories = [];
            foreach (var categoryName in categoryNames)
            {
                foreach (var category in medicineRow.Categories!)
                {
                    if (category == categoryName)
                    {
                        categories.Add(existingCategoriesList.FirstOrDefault(c => c.CategoryName == category)
                            ?? newCategoriesDict!.GetOrAdd(category, new Category { Id = Guid.NewGuid(), CategoryName = category }));
                    }
                }
            }
            #endregion

            #region  Assign related entities
            medicine.Categories = categories;
            medicine.PharmaceuticalCompanies = [pharmaceuticalCompany];
            medicine.DosageForms = [dosageForm];
            medicine.MedicineInBrands = [new MedicineInBrand { Brand = brand, Price = price, PriceUnit = priceUnit, MedicineUrl = medicineRow.MedicineUrl }];
            medicine.ActiveIngredients = activeIngredients;
            medicine.Specification = specification;
            #endregion

            newMedicinesList.Add(medicine);

            #region Add newly created entities to the context
            if (!existingNationsList.Contains(nation)) Context.Nations.Add(nation);
            if (!existingPharmaceuticalCompaniesList.Contains(pharmaceuticalCompany)) Context.PharmaceuticalCompanies.Add(pharmaceuticalCompany);
            if (!existingBrandsList.Contains(brand)) Context.Brands.Add(brand);
            if (!existingDosageFormsList.Contains(dosageForm)) Context.DosageForms.Add(dosageForm);
            if (!existingSpecificationsList.Contains(specification)) Context.Specifications.Add(specification);

            foreach (var activeIngredient in newActiveIngredientsDict.Values)
            {
                if (!existingActiveIngredientsList.Contains(activeIngredient)) Context.ActiveIngredients.Add(activeIngredient);
            }

            foreach (var category in newCategoriesDict.Values)
            {
                if (!existingCategoriesList.Contains(category)) Context.Categories.Add(category);
            }
            #endregion
        }

        // Add new medicines to the context
        await Context.Medicines.AddRangeAsync(newMedicinesList);

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

    public async Task<MedicineDto> CreateFullMedicineAsync(CreateFullMedicineDto createFullMedicineDto)
    {
        await ValidateAsync(createFullMedicineDto);

        var medicine = Mapper.Map<Medicine>(createFullMedicineDto);

        // get related entities from list of ids
        var categories = await GetEntitiesByIdsAsync(createFullMedicineDto.Categories, Context.Categories);
        var pharmaceuticalCompanies = await GetEntitiesByIdsAsync(createFullMedicineDto.PharmaceuticalCompanies, Context.PharmaceuticalCompanies);
        var dosageForms = await GetEntitiesByIdsAsync(createFullMedicineDto.DosageForms, Context.DosageForms);
        var activeIngredients = await GetEntitiesByIdsAsync(createFullMedicineDto.ActiveIngredients, Context.ActiveIngredients);

        // assign related entities
        medicine.Categories = categories;
        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;
        medicine.DosageForms = dosageForms;
        medicine.ActiveIngredients = activeIngredients;

        await Context.Medicines.AddAsync(medicine);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task<MedicinePriceUnitsDto> GetMedicinePriceUnitsAsync()
    {
        var priceUnits = await Context.MedicineInBrands
            .Select(mib => mib.PriceUnit!)
            .Distinct()
            .Where(u => !string.IsNullOrWhiteSpace(u))
            .AsNoTracking()
            .ToListAsync();

        return new MedicinePriceUnitsDto 
        {
            TotalCount = priceUnits.Count,
            PriceUnits = priceUnits 
        };
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
            .ThenInclude(pc => pc.Nation)
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

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicineDto>(medicine);
    }

    public async Task<MedicineDto> UpdateFullMedicineAsync(Guid medicineId, UpdateFullMedicineDto updateFullMedicineDto)
    {
        await ValidateAsync(updateFullMedicineDto);

        var medicine = await Context.Medicines
            .Where(m => !m.IsDeleted)
            .Include(m => m.Categories)
            .Include(m => m.Specification)
            .Include(m => m.PharmaceuticalCompanies)
            .Include(m => m.DosageForms)
            .Include(m => m.MedicineInBrands)
            .Include(m => m.ActiveIngredients)
            .FirstOrDefaultAsync(m => m.Id == medicineId)
            ?? throw new NotFoundException(nameof(Medicine), medicineId);

        Mapper.Map(updateFullMedicineDto, medicine);

        // get related entities from list of ids
        var categories = await GetEntitiesByIdsAsync(updateFullMedicineDto.Categories, Context.Categories);
        var pharmaceuticalCompanies = await GetEntitiesByIdsAsync(updateFullMedicineDto.PharmaceuticalCompanies, Context.PharmaceuticalCompanies);
        var dosageForms = await GetEntitiesByIdsAsync(updateFullMedicineDto.DosageForms, Context.DosageForms);
        var activeIngredients = await GetEntitiesByIdsAsync(updateFullMedicineDto.ActiveIngredients, Context.ActiveIngredients);

        // assign related entities
        medicine.Categories = categories;
        medicine.PharmaceuticalCompanies = pharmaceuticalCompanies;
        medicine.DosageForms = dosageForms;
        medicine.ActiveIngredients = activeIngredients;

        // map and update the medicine in brands
        foreach (var medicineInBrandDto in updateFullMedicineDto.MedicineInBrands)
        {
            var medicineInBrand = await Context.MedicineInBrands
                .FirstOrDefaultAsync(mib => mib.MedicineId == medicineId && mib.BrandId == medicineInBrandDto.BrandId);
            
            if (medicineInBrand == null)
            {
                var newMedicineInBrand = Mapper.Map<MedicineInBrand>(medicineInBrandDto);
                newMedicineInBrand.MedicineId = medicineId;
                Context.MedicineInBrands.Add(newMedicineInBrand);
            }
            else
            {
                Mapper.Map(medicineInBrandDto, medicineInBrand);
                Context.MedicineInBrands.Update(medicineInBrand);
            }
        }

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
        MedicineExcelProperties excelProperties, ExcelPropertyDelimiters excelPropertyDelimiters)
    {
        var dataTable = fileReader.ReadExcelFile(file);

        // represents the number of qualified rows in the excel file
        int exeCount = 0;

        var excelMedicineListToInsert = new List<CreateMedicineFromExcelDto>();

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
                MedicineName = row[excelProperties.MedicineName].ToString(),
                RequirePrescript = row[excelProperties.RequirePrescript].ToString()!.Equals("Có"),
                Image = row[excelProperties.Image].ToString(),
                Specifications = row[excelProperties.Specifications].ToString(),
                DosageForms = row[excelProperties.DosageForms].ToString(),
                PharmaceuticalCompanies = row[excelProperties.PharmaceuticalCompanies].ToString(),
                Brand = row[excelProperties.Brand].ToString(),
                BrandLogo = row[excelProperties.BrandLogo].ToString(),
                BrandUrl = row[excelProperties.BrandUrl].ToString(),
                Price = row[excelProperties.Price].ToString(),
                MedicineUrl = row[excelProperties.MedicineUrl].ToString(),
                Nation = row[excelProperties.Nation].ToString(),
                RegistrationNumber = row[excelProperties.RegistrationNumber].ToString(),

                Categories = row[excelProperties.Categories].ToString()?
                    .Split(excelPropertyDelimiters.CategoryDelimeter)
                    .Select(c => c.Trim()).ToList(),
                ActiveIngredients = row[excelProperties.ActiveIngredients].ToString()?
                    .Split(excelPropertyDelimiters.IngredientDelimeter)
                    .Select(ai => ai.Trim()).ToList(),
            };

            excelMedicineListToInsert.Add(medicine);
            exeCount++;
        }

        // filter out the medicine rows that is duplicated by registration number
        excelMedicineListToInsert = excelMedicineListToInsert
            .GroupBy(m => m.RegistrationNumber)
            .Select(g => g.First())
            .ToList();

        // actual insertion of rows by affected rows in the database
        var affectedRows = await CreateMedicinesFromExcelBatchAsync(excelMedicineListToInsert);

        return new FileExecutionResult
        {
            ExcelExecutionCount = exeCount,
            AffectedRows = affectedRows
        };
    }
}
