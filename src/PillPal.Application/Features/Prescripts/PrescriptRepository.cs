using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Prescripts;

public class PrescriptRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), IPrescriptService
{
    public async Task<PrescriptDto> CreatePrescriptAsync(CreatePrescriptDto createPrescriptDto)
    {
        await ValidateAsync(createPrescriptDto);

        var prescript = Mapper.Map<Prescript>(createPrescriptDto);

        var customer = await Context.Customers
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(Customer), user.Id!);

        prescript.CustomerId = customer.Id;

        await Context.Prescripts.AddAsync(prescript);

        await Context.SaveChangesAsync();

        return Mapper.Map<PrescriptDto>(prescript);
    }

    public async Task DeletePrescriptByIdAsync(Guid prescriptId)
    {
        var prescript = await Context.Prescripts
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == prescriptId) 
            ?? throw new NotFoundException(nameof(Prescript), prescriptId);

        Context.Prescripts.Remove(prescript);

        await Context.SaveChangesAsync();
    }

    public async Task<PrescriptDto> GetPrescriptByIdAsync(Guid prescriptId)
    {
        var prescript = await Context.Prescripts
            .Where(p => !p.IsDeleted)
            .Include(p => p.PrescriptDetails)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == prescriptId) 
            ?? throw new NotFoundException(nameof(Prescript), prescriptId);

        return Mapper.Map<PrescriptDto>(prescript);
    }

    public async Task<IEnumerable<PrescriptDto>> GetPrescriptsAsync(
        PrescriptQueryParameter queryParameter, PrescriptIncludeParameter includeParameter)
    {
        var prescipts = await Context.Prescripts
            .Where(p => !p.IsDeleted)
            .Filter(queryParameter)
            .Include(includeParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<PrescriptDto>>(prescipts);
    }
}
