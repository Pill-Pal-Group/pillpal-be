using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Customers;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        await ValidateAsync(createCustomerDto);

        var customer = Mapper.Map<Customer>(createCustomerDto);

        await Context.Customers.AddAsync(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(CustomerQueryParameter queryParameter)
    {
        var customers = await Context.Customers
            .Include(c => c.IdentityUser)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto updateCustomerDto)
    {
        await ValidateAsync(updateCustomerDto);

        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        Mapper.Map(updateCustomerDto, customer);

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }
}
