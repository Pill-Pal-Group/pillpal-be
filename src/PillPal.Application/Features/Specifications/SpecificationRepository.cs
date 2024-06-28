using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Specifications;

public class SpecificationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ISpecificationService
{
    public async Task<SpecificationDto> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto)
    {
        await ValidateAsync(createSpecificationDto);

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

    public async Task<IEnumerable<SpecificationDto>> GetSpecificationsAsync()
    {
        var specifications = await Context.Specifications
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<SpecificationDto>>(specifications);
    }

    public async Task<SpecificationDto> GetSpecificationByIdAsync(Guid specificationId)
    {
        var specification = await Context.Specifications.FindAsync(specificationId)
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        return Mapper.Map<SpecificationDto>(specification);
    }

    public async Task<SpecificationDto> UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto)
    {
        await ValidateAsync(updateSpecificationDto);

        var specification = await Context.Specifications.FindAsync(specificationId)
            ?? throw new NotFoundException(nameof(Specification), specificationId);

        Mapper.Map(updateSpecificationDto, specification);

        Context.Specifications.Update(specification);

        await Context.SaveChangesAsync();

        return Mapper.Map<SpecificationDto>(specification);
    }

    public async Task<IEnumerable<SpecificationDto>> CreateBulkSpecificationsAsync(IEnumerable<CreateSpecificationDto> createSpecificationDtos)
    {
        await ValidateListAsync(createSpecificationDtos);

        var specifications = Mapper.Map<IEnumerable<Specification>>(createSpecificationDtos);

        await Context.Specifications.AddRangeAsync(specifications);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<SpecificationDto>>(specifications);
    }
}
