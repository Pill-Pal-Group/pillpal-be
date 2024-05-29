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
        var validator = ServiceProvider.GetRequiredService<CreateSpecificationValidator>();

        var validationResult = await validator.ValidateAsync(createSpecificationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var specification = Mapper.Map<Specification>(createSpecificationDto);

        await Context.Specifications.AddAsync(specification);

        await Context.SaveChangesAsync();

        return Mapper.Map<SpecificationDto>(specification);
    }

    public async Task DeleteSpecificationAsync(Guid specificationId)
    {
        var specification = await Context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        Context.Specifications.Remove(specification);

        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SpecificationDto>> GetAllSpecificationsAsync()
    {
        var specifications = await Context.Specifications.ToListAsync();

        return Mapper.Map<IEnumerable<SpecificationDto>>(specifications);
    }

    public async Task<SpecificationDto> GetSpecificationByIdAsync(Guid specificationId)
    {
        var specification = await Context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        return Mapper.Map<SpecificationDto>(specification);
    }

    public async Task UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateSpecificationValidator>();

        var validationResult = await validator.ValidateAsync(updateSpecificationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var specification = await Context.Specifications.FindAsync(specificationId) 
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        Mapper.Map(updateSpecificationDto, specification);

        await Context.SaveChangesAsync();
    }
}
