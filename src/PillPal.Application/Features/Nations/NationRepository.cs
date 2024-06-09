using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Nations;

public class NationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), INationService
{
    public async Task<NationDto> CreateNationAsync(CreateNationDto createNationDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateNationValidator>();

        var validationResult = await validator.ValidateAsync(createNationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var nation = Mapper.Map<Nation>(createNationDto);

        await Context.Nations.AddAsync(nation);

        await Context.SaveChangesAsync();

        return Mapper.Map<NationDto>(nation);
    }

    public async Task DeleteNationAsync(Guid nationId)
    {
        var nation = await Context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        nation.IsDeleted = true;

        Context.Nations.Update(nation);

        await Context.SaveChangesAsync();
    }

    public async Task<NationDto> GetNationByIdAsync(Guid nationId)
    {
        var nation = await Context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        return Mapper.Map<NationDto>(nation);
    }

    public async Task<IEnumerable<NationDto>> GetNationsAsync(NationQueryParameter queryParameter)
    {
        var nations = Context.Nations
            .Where(n => !n.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<NationDto>>(nations);
    }

    public async Task<NationDto> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateNationValidator>();

        var validationResult = await validator.ValidateAsync(updateNationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var nation = await Context.Nations
            .Where(n => n.Id == nationId && !n.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Nation), nationId);

        Mapper.Map(updateNationDto, nation);

        Context.Nations.Update(nation);

        await Context.SaveChangesAsync();

        return Mapper.Map<NationDto>(nation);
    }
}
