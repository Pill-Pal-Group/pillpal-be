using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.TermsOfServices;

public class TermsOfServiceRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ITermsOfService
{
    public async Task<IEnumerable<TermsOfServiceDto>> CreateBulkTermsOfServicesAsync(IEnumerable<CreateTermsOfServiceDto> createTermsOfServiceDtos)
    {
        await ValidateListAsync(createTermsOfServiceDtos);

        var tosList = Mapper.Map<IEnumerable<TermsOfService>>(createTermsOfServiceDtos);

        await Context.TermsOfServices.AddRangeAsync(tosList);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<TermsOfServiceDto>>(tosList);
    }

    public async Task<TermsOfServiceDto> CreateTermsOfServiceAsync(CreateTermsOfServiceDto createTermsOfServiceDto)
    {
        await ValidateAsync(createTermsOfServiceDto);

        var tos = Mapper.Map<TermsOfService>(createTermsOfServiceDto);

        await Context.TermsOfServices.AddAsync(tos);

        await Context.SaveChangesAsync();

        return Mapper.Map<TermsOfServiceDto>(tos);
    }

    public async Task DeleteTermsOfServiceAsync(Guid termsOfServiceId)
    {
        var tos = await Context.TermsOfServices
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == termsOfServiceId)
            ?? throw new NotFoundException(nameof(TermsOfService), termsOfServiceId);

        Context.TermsOfServices.Remove(tos);

        await Context.SaveChangesAsync();
    }

    public async Task<TermsOfServiceDto> GetTermsOfServiceByIdAsync(Guid termsOfServiceId)
    {
        var tos = await Context.TermsOfServices
            .Where(t => !t.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == termsOfServiceId)
            ?? throw new NotFoundException(nameof(TermsOfService), termsOfServiceId);

        return Mapper.Map<TermsOfServiceDto>(tos);
    }

    public async Task<IEnumerable<TermsOfServiceDto>> GetTermsOfServicesAsync(TermsOfServiceQueryParameter queryParameter)
    {
        var tosList = await Context.TermsOfServices
            .Where(t => !t.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<TermsOfServiceDto>>(tosList);
    }

    public async Task<TermsOfServiceDto> UpdateTermsOfServiceAsync(Guid termsOfServiceId, UpdateTermsOfServiceDto updateTermsOfServiceDto)
    {
        await ValidateAsync(updateTermsOfServiceDto);

        var tos = await Context.TermsOfServices
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == termsOfServiceId)
            ?? throw new NotFoundException(nameof(TermsOfService), termsOfServiceId);

        Mapper.Map(updateTermsOfServiceDto, tos);

        Context.TermsOfServices.Update(tos);

        await Context.SaveChangesAsync();

        return Mapper.Map<TermsOfServiceDto>(tos);
    }
}
