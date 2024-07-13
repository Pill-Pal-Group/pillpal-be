using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Paginations;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.DosageForms;

public class DosageFormRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IDosageFormService
{
    public async Task<IEnumerable<DosageFormDto>> CreateBulkDosageFormsAsync(IEnumerable<CreateDosageFormDto> createDosageFormDtos)
    {
        await ValidateListAsync(createDosageFormDtos);

        var dosageForms = Mapper.Map<IEnumerable<DosageForm>>(createDosageFormDtos);

        await Context.DosageForms.AddRangeAsync(dosageForms);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<DosageFormDto>>(dosageForms);
    }

    public async Task<DosageFormDto> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto)
    {
        await ValidateAsync(createDosageFormDto);

        var dosageForm = Mapper.Map<DosageForm>(createDosageFormDto);

        await Context.DosageForms.AddAsync(dosageForm);

        await Context.SaveChangesAsync();

        return Mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task DeleteDosageFormAsync(Guid dosageFormId)
    {
        var dosageForm = await Context.DosageForms
            .Where(d => !d.IsDeleted)
            .FirstOrDefaultAsync(d => d.Id == dosageFormId)
            ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        Context.DosageForms.Remove(dosageForm);

        await Context.SaveChangesAsync();
    }

    public async Task<DosageFormDto> GetDosageFormByIdAsync(Guid dosageFormId)
    {
        var dosageForm = await Context.DosageForms
            .AsNoTracking()
            .Where(d => !d.IsDeleted)
            .FirstOrDefaultAsync(d => d.Id == dosageFormId)
            ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        return Mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task<PaginationResponse<DosageFormDto>> GetDosageFormsAsync(DosageFormQueryParameter queryParameter)
    {
        var dosageForms = await Context.DosageForms
            .AsNoTracking()
            .Where(d => !d.IsDeleted)
            .Filter(queryParameter)
            .ToPaginationResponseAsync<DosageForm, DosageFormDto>(queryParameter, Mapper);

        return dosageForms;
    }

    public async Task<DosageFormDto> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto)
    {
        await ValidateAsync(updateDosageFormDto);

        var dosageForm = await Context.DosageForms
            .Where(d => !d.IsDeleted)
            .FirstOrDefaultAsync(d => d.Id == dosageFormId)
            ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        Mapper.Map(updateDosageFormDto, dosageForm);

        await Context.SaveChangesAsync();

        return Mapper.Map<DosageFormDto>(dosageForm);
    }
}
