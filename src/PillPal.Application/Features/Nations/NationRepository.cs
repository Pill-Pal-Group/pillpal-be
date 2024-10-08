﻿namespace PillPal.Application.Features.Nations;

public class NationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), INationService
{
    public async Task<IEnumerable<NationDto>> CreateBulkNationsAsync(IEnumerable<CreateNationDto> createNationDtos)
    {
        await ValidateListAsync(createNationDtos);

        var nations = Mapper.Map<IEnumerable<Nation>>(createNationDtos);

        await Context.Nations.AddRangeAsync(nations);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<NationDto>>(nations);
    }

    public async Task<NationDto> CreateNationAsync(CreateNationDto createNationDto)
    {
        await ValidateAsync(createNationDto);

        var nation = Mapper.Map<Nation>(createNationDto);

        await Context.Nations.AddAsync(nation);

        await Context.SaveChangesAsync();

        return Mapper.Map<NationDto>(nation);
    }

    public async Task DeleteNationAsync(Guid nationId)
    {
        var nation = await Context.Nations
            .Where(n => !n.IsDeleted)
            .FirstOrDefaultAsync(n => n.Id == nationId)
            ?? throw new NotFoundException(nameof(Nation), nationId);

        Context.Nations.Remove(nation);

        await Context.SaveChangesAsync();
    }

    public async Task<NationDto> GetNationByIdAsync(Guid nationId)
    {
        var nation = await Context.Nations
            .Where(n => !n.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == nationId)
            ?? throw new NotFoundException(nameof(Nation), nationId);

        return Mapper.Map<NationDto>(nation);
    }

    public async Task<IEnumerable<NationDto>> GetNationsAsync()
    {
        var nations = await Context.Nations
            .Where(n => !n.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<NationDto>>(nations);
    }

    public async Task<NationDto> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto)
    {
        await ValidateAsync(updateNationDto);

        var nation = await Context.Nations
            .Where(n => !n.IsDeleted)
            .FirstOrDefaultAsync(n => n.Id == nationId)
            ?? throw new NotFoundException(nameof(Nation), nationId);

        Mapper.Map(updateNationDto, nation);

        Context.Nations.Update(nation);

        await Context.SaveChangesAsync();

        return Mapper.Map<NationDto>(nation);
    }
}
