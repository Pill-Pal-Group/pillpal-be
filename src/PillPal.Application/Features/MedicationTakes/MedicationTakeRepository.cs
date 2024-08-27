namespace PillPal.Application.Features.MedicationTakes;

public class MedicationTakeRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IMedicationTakeService
{
    public async Task<IEnumerable<MedicationTakesDto>> CreateMedicationTakeAsync(Guid prescriptId)
    {
        var prescript = await Context.Prescripts
            .AsNoTracking()
            .Include(p => p.PrescriptDetails)
            .Include(p => p.Customer)
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == prescriptId)
            ?? throw new NotFoundException(nameof(Prescript), prescriptId);

        var allMedicationTakes = new List<MedicationTake>();

        foreach (var prescriptDetail in prescript.PrescriptDetails)
        {
            var existingMedicationTakes = await Context.MedicationTakes
                .Where(mt => mt.PrescriptDetailId == prescriptDetail.Id)
                .ToListAsync();

            if (existingMedicationTakes.Any())
            {
                throw new BadRequestException("Medication Take is already created.");
            }

            var totalDays = (prescriptDetail.DateEnd - prescriptDetail.DateStart).TotalDays;

            var dosageDict = new Dictionary<string, double>
            {
                { "Morning", prescriptDetail.MorningDose },
                { "Noon", prescriptDetail.NoonDose },
                { "Afternoon", prescriptDetail.AfternoonDose },
                { "Night", prescriptDetail.NightDose }
            };

            var timeTakeDict = new Dictionary<string, TimeOnly>
            {
                { "Morning", prescript.Customer!.BreakfastTime },
                { "Noon", prescript.Customer!.LunchTime },
                { "Afternoon", prescript.Customer!.AfternoonTime },
                { "Night", prescript.Customer!.DinnerTime }
            };

            for (int i = 0; i < totalDays; i++)
            {
                var dateTake = prescriptDetail.DateStart.AddDays(i);

                foreach (var dose in dosageDict)
                {
                    if (dose.Value == 0)
                    {
                        continue;
                    }

                    var timeTake = timeTakeDict[dose.Key];

                    if (prescriptDetail.DosageInstruction == DosageInstructionEnums.Aftermeal.ToString())
                    {
                        timeTake = timeTake.AddMinutes(prescript.Customer!.MealTimeOffset.TotalMinutes);
                    }

                    if (prescriptDetail.DosageInstruction == DosageInstructionEnums.Beforemeal.ToString())
                    {
                        timeTake = timeTake.AddMinutes(-prescript.Customer!.MealTimeOffset.TotalMinutes);
                    }

                    var medicationTake = new MedicationTake
                    {
                        DateTake = dateTake,
                        TimeTake = timeTake.ToString("HH:mm"),
                        Dose = dose.Value,
                        PrescriptDetailId = prescriptDetail.Id
                    };

                    allMedicationTakes.Add(medicationTake);
                }
            }
        }

        await Context.MedicationTakes.AddRangeAsync(allMedicationTakes);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<MedicationTakesDto>>(allMedicationTakes);
    }

    public async Task<IEnumerable<MedicationTakesListDto>> GetMedicationTakesAsync(Guid prescriptId, DateTimeOffset? dateTake)
    {
        var prescript = await Context.Prescripts
            .AsNoTracking()
            .Include(p => p.PrescriptDetails)
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == prescriptId)
            ?? throw new NotFoundException(nameof(Prescript), prescriptId);

        var allMedicationTakes = new List<MedicationTakesListDto>();

        foreach (var prescriptDetail in prescript.PrescriptDetails)
        {
            var medicineName = prescriptDetail.MedicineName;
            var medicineImage = prescriptDetail.MedicineImage;

            var medicationTakesQuery = Context.MedicationTakes
                .Where(mt => mt.PrescriptDetailId == prescriptDetail.Id && !mt.IsDeleted);

            if (dateTake.HasValue)
            {
                medicationTakesQuery = medicationTakesQuery
                    .Where(mt => mt.DateTake.Date == dateTake.Value.Date);
            }

            var medicationTakes = await medicationTakesQuery
                .AsNoTracking()
                .ToListAsync();

            allMedicationTakes.Add(new MedicationTakesListDto
            {
                MedicineName = medicineName,
                MedicineImage = medicineImage,
                MedicationTakes = Mapper.Map<IEnumerable<MedicationTakesDto>>(medicationTakes)
            });
        }

        return allMedicationTakes;
    }

    public async Task DeleteMedicationTakeAsync(Guid medicationTakeId)
    {
        var medicationTake = await Context.MedicationTakes
            .Where(mt => !mt.IsDeleted)
            .FirstOrDefaultAsync(mt => mt.Id == medicationTakeId)
            ?? throw new NotFoundException(nameof(MedicationTake), medicationTakeId);

        Context.MedicationTakes.Remove(medicationTake);

        await Context.SaveChangesAsync();
    }

    public async Task<MedicationTakesDto> CreateMedicationTakeAsync(CreateMedicationTakesDto createMedicationTakesDto)
    {
        await ValidateAsync(createMedicationTakesDto);

        var prescriptDetail = await Context.PrescriptDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(pd => pd.Id == createMedicationTakesDto.PrescriptDetailId)
            ?? throw new NotFoundException(nameof(PrescriptDetail), createMedicationTakesDto.PrescriptDetailId);

        var medicationTake = Mapper.Map<MedicationTake>(createMedicationTakesDto);

        await Context.MedicationTakes.AddAsync(medicationTake);

        await Context.SaveChangesAsync();

        return Mapper.Map<MedicationTakesDto>(medicationTake);
    }

    public async Task<MedicationTakesDto> GetIndividualMedicationTakesAsync(Guid medicationTakeId)
    {
        var medicationTake = await Context.MedicationTakes
            .AsNoTracking()
            .FirstOrDefaultAsync(mt => mt.Id == medicationTakeId)
            ?? throw new NotFoundException(nameof(MedicationTake), medicationTakeId);

        return Mapper.Map<MedicationTakesDto>(medicationTake);
    }
}
