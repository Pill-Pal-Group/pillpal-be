using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Customers;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICustomerService
{
    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == customerId)
            ?? throw new NotFoundException(nameof(Customer), customerId);

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
            .FirstOrDefaultAsync(c => c.Id == customerId)
            ?? throw new NotFoundException(nameof(Customer), customerId);

        Mapper.Map(updateCustomerDto, customer);

        customer.IdentityUser!.PhoneNumber = updateCustomerDto.PhoneNumber;

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerMealTimeDto> UpdateCustomerMealTimeAsync(Guid customerId, UpdateCustomerMealTimeDto updateCustomerMealTimeDto)
    {
        await ValidateAsync(updateCustomerMealTimeDto);

        var customer = await Context.Customers
            .FirstOrDefaultAsync(c => c.Id == customerId)
            ?? throw new NotFoundException(nameof(Customer), customerId);

        Mapper.Map(updateCustomerMealTimeDto, customer);

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerMealTimeDto>(customer);
    }
}
