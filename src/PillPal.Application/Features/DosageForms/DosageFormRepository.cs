using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.DosageForms;

public class DosageFormRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IDosageFormService
{
    public async Task<DosageFormDto> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateDosageFormValidator>();

        var validationResult = await validator.ValidateAsync(createDosageFormDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var dosageForm = Mapper.Map<DosageForm>(createDosageFormDto);

        await Context.DosageForms.AddAsync(dosageForm);

        await Context.SaveChangesAsync();

        return Mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task DeleteDosageFormAsync(Guid dosageFormId)
    {
        var dosageForm = await Context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        Context.DosageForms.Remove(dosageForm);

        await Context.SaveChangesAsync();
    }

    public async Task<DosageFormDto> GetDosageFormByIdAsync(Guid dosageFormId)
    {
        var dosageForm = await Context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        return Mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task<IEnumerable<DosageFormDto>> GetDosageFormsAsync()
    {
        var dosageForms = await Context.DosageForms
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<DosageFormDto>>(dosageForms);
    }

    public async Task<DosageFormDto> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateDosageFormValidator>();

        var validationResult = await validator.ValidateAsync(updateDosageFormDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var dosageForm = await Context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        Mapper.Map(updateDosageFormDto, dosageForm);

        await Context.SaveChangesAsync();

        return Mapper.Map<DosageFormDto>(dosageForm);
    }
}
