using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.MedicationTakes;

public class MedicationTakeRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IMedicationTakeService
{
    public async Task<IEnumerable<MedicationTakesDto>> CreateMedicationTakeAsync(Guid prescriptId)
    {
        var prescript = await Context.Prescripts
            .Where(p => p.Id == prescriptId && !p.IsDeleted)
            .Include(p => p.PrescriptDetails)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Prescript), prescriptId);

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
            var doesPerTake = prescriptDetail.Total / totalDays;

            for (int i = 0; i < totalDays; i++)
            {
                var dateTake = prescriptDetail.DateStart.AddDays(i);
                var timeTake = "3meal";
                var dose = doesPerTake.ToString();

                var medicationTake = new MedicationTake
                {
                    DateTake = dateTake,
                    TimeTake = timeTake,
                    Dose = dose,
                    PrescriptDetailId = prescriptDetail.Id
                };

                allMedicationTakes.Add(medicationTake);
            }

        }

        await Context.MedicationTakes.AddRangeAsync(allMedicationTakes);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<MedicationTakesDto>>(allMedicationTakes);
    }

    public async Task<IEnumerable<MedicationTakesDto>> GetMedicationTakesAsync(Guid prescriptId, DateTimeOffset? dateTake)
    {
        var prescript = await Context.Prescripts
            .Where(p => p.Id == prescriptId && !p.IsDeleted)
            .Include(p => p.PrescriptDetails)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Prescript), prescriptId);
        
        var allMedicationTakes = new List<MedicationTake>();

        foreach (var prescriptDetail in prescript.PrescriptDetails)
        {
            var medicationTakesQuery = Context.MedicationTakes
                .Where(mt => mt.PrescriptDetailId == prescriptDetail.Id);

            if (dateTake.HasValue)
            {
                medicationTakesQuery = medicationTakesQuery
                    .Where(mt => mt.DateTake.Date == dateTake.Value.Date);
            }

            var medicationTakes = await medicationTakesQuery
                .AsNoTracking()
                .ToListAsync();

            allMedicationTakes.AddRange(medicationTakes);
        }
        
        return Mapper.Map<IEnumerable<MedicationTakesDto>>(allMedicationTakes);
    }
}
