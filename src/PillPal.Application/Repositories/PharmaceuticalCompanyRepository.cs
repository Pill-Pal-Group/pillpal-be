using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.PharmaceuticalCompanies;

namespace PillPal.Application.Repositories;

public class PharmaceuticalCompanyRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPharmaceuticalCompanyService
{
    public async Task<PharmaceuticalCompanyDto> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreatePharmaceuticalCompanyValidator>();

        var validationResult = await validator.ValidateAsync(createPharmaceuticalCompanyDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmaceuticalCompany = _mapper.Map<PharmaceuticalCompany>(createPharmaceuticalCompanyDto);

        await _context.PharmaceuticalCompanies.AddAsync(pharmaceuticalCompany);

        await _context.SaveChangesAsync();

        return _mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }

    public async Task DeletePharmaceuticalCompanyAsync(Guid nationId)
    {
        var pharmaceuticalCompany = await _context.PharmaceuticalCompanies
            .Where(b => b.Id == nationId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), nationId);

        pharmaceuticalCompany.IsDeleted = true;

        _context.PharmaceuticalCompanies.Update(pharmaceuticalCompany);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PharmaceuticalCompanyDto>> GetPharmaceuticalCompaniesAsync()
    {
        var pharmaceuticalCompanies = await _context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PharmaceuticalCompanyDto>>(pharmaceuticalCompanies);
    }

    public async Task<PharmaceuticalCompanyDto> GetPharmaceuticalCompanyByIdAsync(Guid nationId)
    {
        var pharmaceuticalCompany = await _context.PharmaceuticalCompanies
            .Include(b => b.Nation)
            .Where(b => b.Id == nationId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), nationId);

        return _mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }

    public async Task<PharmaceuticalCompanyDto> UpdatePharmaceuticalCompanyAsync(Guid nationId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdatePharmaceuticalCompanyValidator>();

        var validationResult = await validator.ValidateAsync(updatePharmaceuticalCompanyDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmaceuticalCompany = await _context.PharmaceuticalCompanies
            .Where(b => b.Id == nationId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmaceuticalCompany), nationId);

        _mapper.Map(updatePharmaceuticalCompanyDto, pharmaceuticalCompany);

        _context.PharmaceuticalCompanies.Update(pharmaceuticalCompany);

        await _context.SaveChangesAsync();

        return _mapper.Map<PharmaceuticalCompanyDto>(pharmaceuticalCompany);
    }
}
