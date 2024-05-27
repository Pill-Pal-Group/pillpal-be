using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Specifications;

namespace PillPal.Application.Repositories;

public class SpecificationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ISpecificationService
{
    public async Task<SpecificationDto> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateSpecificationValidator>();

        var validationResult = await validator.ValidateAsync(createSpecificationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var specification = _mapper.Map<Specification>(createSpecificationDto);

        await _context.Specifications.AddAsync(specification);

        await _context.SaveChangesAsync();

        return _mapper.Map<SpecificationDto>(specification);
    }

    public async Task DeleteSpecificationAsync(Guid specificationId)
    {
        var specification = await _context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        _context.Specifications.Remove(specification);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SpecificationDto>> GetAllSpecificationsAsync()
    {
        var specifications = await _context.Specifications.ToListAsync();

        return _mapper.Map<IEnumerable<SpecificationDto>>(specifications);
    }

    public async Task<SpecificationDto> GetSpecificationByIdAsync(Guid specificationId)
    {
        var specification = await _context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        return _mapper.Map<SpecificationDto>(specification);
    }

    public async Task UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateSpecificationValidator>();

        var validationResult = await validator.ValidateAsync(updateSpecificationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var specification = await _context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        _mapper.Map(updateSpecificationDto, specification);

        await _context.SaveChangesAsync();
    }
}
