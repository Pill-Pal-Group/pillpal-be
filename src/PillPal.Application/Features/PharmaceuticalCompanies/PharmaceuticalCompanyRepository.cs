using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.PharmaceuticalCompanies;

public class PharmaceuticalCompanyRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPharmaceuticalCompanyService
{
    public async Task<PharmaceuticalCompanyDto> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreatePharmaceuticalCompanyValidator>();

        var validationResult = await validator.ValidateAsync(createPharmaceuticalCompanyDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmaceuticalCompany = Mapper.Map<PharmaceuticalCompany>(createPharmaceuticalCompanyDto);

        await Context.PharmaceuticalCompanies.AddAsync(pharmaceuticalCompany);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }

    public async Task DeletePharmaceuticalCompanyAsync(Guid companyId)
    {
        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Where(b => b.Id == companyId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        pharmaceuticalCompany.IsDeleted = true;

        Context.PharmaceuticalCompanies.Update(pharmaceuticalCompany);

        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PharmaceuticalCompanyDto>> GetPharmaceuticalCompaniesAsync()
    {
        var pharmaceuticalCompanies = await Context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .Where(b => !b.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<PharmaceuticalCompanyDto>>(pharmaceuticalCompanies);
    }

    public async Task<PharmaceuticalCompanyDto> GetPharmaceuticalCompanyByIdAsync(Guid companyId)
    {
        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .Where(b => b.Id == companyId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        return Mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }

    public async Task<PharmaceuticalCompanyDto> UpdatePharmaceuticalCompanyAsync(Guid companyId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdatePharmaceuticalCompanyValidator>();

        var validationResult = await validator.ValidateAsync(updatePharmaceuticalCompanyDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmaceuticalCompany = await Context.PharmaceuticalCompanies
            .Where(b => b.Id == companyId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), companyId);

        Mapper.Map(updatePharmaceuticalCompanyDto, pharmaceuticalCompany);

        Context.PharmaceuticalCompanies.Update(pharmaceuticalCompany);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }
}
