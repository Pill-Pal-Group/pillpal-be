namespace PillPal.Application.Features.Customers;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
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

    public async Task<CustomerDto> GetCustomerInfoAsync()
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(ApplicationUser), user.Id!);

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

    public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
    {
        await ValidateAsync(updateCustomerDto);

        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(ApplicationUser), user.Id!);

        Mapper.Map(updateCustomerDto, customer);

        customer.IdentityUser!.PhoneNumber = updateCustomerDto.PhoneNumber;

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerDto>(customer);
    }

    public async Task UpdateCustomerDeviceTokenAsync(CustomerDeviceTokenDto customerDeviceTokenDto)
    {
        var customer = await Context.Customers
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(ApplicationUser), user.Id!);

        customer.DeviceToken = customerDeviceTokenDto.DeviceToken;

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();
    }

    public async Task<CustomerMealTimeDto> UpdateCustomerMealTimeAsync(UpdateCustomerMealTimeDto updateCustomerMealTimeDto)
    {
        await ValidateAsync(updateCustomerMealTimeDto);

        var customer = await Context.Customers
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(ApplicationUser), user.Id!);

        Mapper.Map(updateCustomerMealTimeDto, customer);

        Context.Customers.Update(customer);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerMealTimeDto>(customer);
    }
}
