namespace PillPal.Application.Features.PharmaceuticalCompanies;

public class PharmaceuticalCompanyRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPharmaceuticalCompanyService
{
    public async Task<PharmaceuticalCompanyDto> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto)
    {
        await ValidateAsync(createPharmaceuticalCompanyDto);

        var pharmaceuticalCompany = Mapper.Map<PharmaceuticalCompany>(createPharmaceuticalCompanyDto);

        await Context.PharmaceuticalCompanies.AddAsync(pharmaceuticalCompany);

        await Context.SaveChangesAsync();

        var createdPharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == pharmaceuticalCompany.Id);

        return Mapper.Map<PharmaceuticalCompanyDto>(createdPharmaceuticalCompany);
    }

    public async Task DeletePharmaceuticalCompanyAsync(Guid companyId)
    {
        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == companyId)
            ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        Context.PharmaceuticalCompanies.Remove(pharmaceuticalCompany);

        await Context.SaveChangesAsync();
    }

    public async Task<PaginationResponse<PharmaceuticalCompanyDto>>
        GetPharmaceuticalCompaniesAsync(PharmaceuticalCompanyQueryParameter queryParameter)
    {
        await ValidateAsync(queryParameter);

        var pharmaceuticalCompanies = await Context.PharmaceuticalCompanies
            .AsNoTracking()
            .Where(b => !b.IsDeleted)
            .Include(b => b.Nation)
            .Filter(queryParameter)
            .ToPaginationResponseAsync<PharmaceuticalCompany, PharmaceuticalCompanyDto>(queryParameter, Mapper);

        return pharmaceuticalCompanies;
    }

    public async Task<PharmaceuticalCompanyDto> GetPharmaceuticalCompanyByIdAsync(Guid companyId)
    {
        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .Where(b => !b.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == companyId)
            ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        return Mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }

    public async Task<PharmaceuticalCompanyDto> UpdatePharmaceuticalCompanyAsync(Guid companyId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto)
    {
        await ValidateAsync(updatePharmaceuticalCompanyDto);

        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Where(b => !b.IsDeleted)
            .Include(b => b.Nation)
            .FirstOrDefaultAsync(b => b.Id == companyId)
            ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        Mapper.Map(updatePharmaceuticalCompanyDto, pharmaceuticalCompany);

        Context.PharmaceuticalCompanies.Update(pharmaceuticalCompany);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }
}
