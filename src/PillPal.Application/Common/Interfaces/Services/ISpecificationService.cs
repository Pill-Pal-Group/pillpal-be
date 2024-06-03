using PillPal.Application.Dtos.Specifications;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ISpecificationService
{
    Task<SpecificationDto> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto);
    Task DeleteSpecificationAsync(Guid specificationId);
    Task<IEnumerable<SpecificationDto>> GetAllSpecificationsAsync();
    Task<SpecificationDto> GetSpecificationByIdAsync(Guid specificationId);
    Task UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto);
}
