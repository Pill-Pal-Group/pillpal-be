using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Customers;

namespace PillPal.Application.Repositories;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateCustomerValidator>();

        var validationResult = await validator.ValidateAsync(createCustomerDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var customer = Mapper.Map<Customer>(createCustomerDto);

        await Context.Customers.AddAsync(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
    {
        var customers = await Context.Customers
            .Include(c => c.IdentityUser)
            .ToListAsync();

        return Mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto updateCustomerDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateCustomerValidator>();

        var validationResult = await validator.ValidateAsync(updateCustomerDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        Mapper.Map(updateCustomerDto, customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }
}
