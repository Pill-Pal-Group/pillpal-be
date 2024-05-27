using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.DosageForms;

namespace PillPal.Application.Repositories;

public class DosageFormRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IDosageFormService
{
    public async Task<DosageFormDto> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateDosageFormValidator>();

        var validationResult = await validator.ValidateAsync(createDosageFormDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var dosageForm = _mapper.Map<DosageForm>(createDosageFormDto);

        await _context.DosageForms.AddAsync(dosageForm);

        await _context.SaveChangesAsync();

        return _mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task DeleteDosageFormAsync(Guid dosageFormId)
    {
        var dosageForm = await _context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);
    
        _context.DosageForms.Remove(dosageForm);

        await _context.SaveChangesAsync();
    }

    public async Task<DosageFormDto> GetDosageFormByIdAsync(Guid dosageFormId)
    {
        var dosageForm = await _context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        return _mapper.Map<DosageFormDto>(dosageForm);
    }

    public async Task<IEnumerable<DosageFormDto>> GetDosageFormsAsync()
    {
        var dosageForms = await _context.DosageForms
            .ToListAsync();

        return _mapper.Map<IEnumerable<DosageFormDto>>(dosageForms);
    }

    public async Task<DosageFormDto> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateDosageFormValidator>();

        var validationResult = await validator.ValidateAsync(updateDosageFormDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var dosageForm = await _context.DosageForms
            .FindAsync(dosageFormId) ?? throw new NotFoundException(nameof(DosageForm), dosageFormId);

        _mapper.Map(updateDosageFormDto, dosageForm);

        await _context.SaveChangesAsync();

        return _mapper.Map<DosageFormDto>(dosageForm);
    }
}
