using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Nations;

namespace PillPal.Application.Repositories;

public class NationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), INationService
{
    public async Task<NationDto> CreateNationAsync(CreateNationDto createNationDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateNationValidator>();

        var validationResult = await validator.ValidateAsync(createNationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var nation = _mapper.Map<Nation>(createNationDto);

        await _context.Nations.AddAsync(nation);

        await _context.SaveChangesAsync();

        return _mapper.Map<NationDto>(nation);
    }

    public async Task DeleteNationAsync(Guid nationId)
    {
        var nation = await _context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        nation.IsDeleted = true;

        _context.Nations.Update(nation);

        await _context.SaveChangesAsync();
    }

    public async Task<NationDto> GetNationByIdAsync(Guid nationId)
    {
        var nation = await _context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        return _mapper.Map<NationDto>(nation);
    }

    public async Task<IEnumerable<NationDto>> GetNationsAsync()
    {
        var nations = await _context.Nations
            .Where(n => !n.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<NationDto>>(nations);
    }

    public async Task<NationDto> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateNationValidator>();

        var validationResult = await validator.ValidateAsync(updateNationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var nation = await _context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        _mapper.Map(updateNationDto, nation);

        _context.Nations.Update(nation);

        await _context.SaveChangesAsync();

        return _mapper.Map<NationDto>(nation);
    }
}
