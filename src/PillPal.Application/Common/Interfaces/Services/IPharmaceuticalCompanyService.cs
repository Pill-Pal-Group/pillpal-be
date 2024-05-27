using PillPal.Application.Dtos.PharmaceuticalCompanies;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPharmaceuticalCompanyService
{
    Task<PharmaceuticalCompanyDto> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto);
    Task DeletePharmaceuticalCompanyAsync(Guid nationId);
    Task<PharmaceuticalCompanyDto> GetPharmaceuticalCompanyByIdAsync(Guid nationId);
    Task<IEnumerable<PharmaceuticalCompanyDto>> GetPharmaceuticalCompaniesAsync();
    Task<PharmaceuticalCompanyDto> UpdatePharmaceuticalCompanyAsync(Guid nationId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto);
}
