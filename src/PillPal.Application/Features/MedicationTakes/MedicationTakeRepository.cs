using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;
using PillPal.Core.Enums;

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
            .FirstOrDefaultAsync(p => p.Id == prescriptId && !p.IsDeleted) 
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
                        timeTake = timeTake.AddMinutes(prescript.Customer!.MealTimeOffset.Minute);
                    }

                    if (prescriptDetail.DosageInstruction == DosageInstructionEnums.Beforemeal.ToString())
                    {
                        timeTake = timeTake.AddMinutes(-prescript.Customer!.MealTimeOffset.Minute);
                    }

                    var medicationTake = new MedicationTake
                    {
                        DateTake = dateTake,
                        TimeTake = timeTake.ToShortTimeString(),
                        Dose = dose.Value.ToString(),
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
            .FirstOrDefaultAsync(p => p.Id == prescriptId && !p.IsDeleted) 
            ?? throw new NotFoundException(nameof(Prescript), prescriptId);

        var allMedicationTakes = new List<MedicationTakesListDto>();

        foreach (var prescriptDetail in prescript.PrescriptDetails)
        {
            var medicineName = prescriptDetail.MedicineName;

            var medicationTakesQuery = Context.MedicationTakes
                .Where(mt => mt.PrescriptDetailId == prescriptDetail.Id
                        && !mt.IsDeleted);

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
                MedicationTakes = Mapper.Map<IEnumerable<MedicationTakesDto>>(medicationTakes)
            });
        }

        return allMedicationTakes;
    }

    public async Task DeleteMedicationTakeAsync(Guid medicationTakeId)
    {
        var medicationTake = await Context.MedicationTakes
            .FirstOrDefaultAsync(mt => mt.Id == medicationTakeId && !mt.IsDeleted)
            ?? throw new NotFoundException(nameof(MedicationTake), medicationTakeId);

        Context.MedicationTakes.Remove(medicationTake);

        await Context.SaveChangesAsync();
    }
}
