namespace PillPal.Application.Features.Customers;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), ICustomerService
{
    private const int MaxAllowedPackagesPerCustomerAsGivenTime = 1;

    private async Task<Dictionary<Guid, string?>> GetCurrentCustomerPackageAsync(List<Guid> customerId)
    {
        var customerPackage = await Context.CustomerPackages
            .Where(cp => customerId.Contains(cp.CustomerId)
                && cp.PaymentStatus == (int)PaymentStatusEnums.PAID
                && !cp.IsExpired)
            .Include(cp => cp.PackageCategory)
            .Select(cp => new
            {
                cp.CustomerId,
                cp.PackageCategory!.PackageName,
            })
            .AsNoTracking()
            .ToListAsync();

        var packageNamesByCustomerId = customerPackage
            .GroupBy(cp => cp.CustomerId)
            .ToDictionary(
                cp => cp.Key,
                cp => cp.Count() > MaxAllowedPackagesPerCustomerAsGivenTime
                    // In case of data inconsistency, cause of multiple packages for a customer
                    // return error as a way to alert that
                    ? "error" : cp.FirstOrDefault()?.PackageName
            );

        return packageNamesByCustomerId;
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == customerId)
            ?? throw new NotFoundException(nameof(Customer), customerId);

        var customerResponse = Mapper.Map<CustomerDto>(customer);

        customerResponse.CustomerPackage = (await GetCurrentCustomerPackageAsync([customer.Id]))
            .FirstOrDefault(cp => cp.Key == customer.Id).Value;

        return customerResponse;
    }

    public async Task<CustomerDto> GetCustomerInfoAsync()
    {
        var customer = await Context.Customers
            .Include(c => c.IdentityUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IdentityUserId == Guid.Parse(user.Id!))
            ?? throw new NotFoundException(nameof(ApplicationUser), user.Id!);

        var customerResponse = Mapper.Map<CustomerDto>(customer);

        customerResponse.CustomerPackage = (await GetCurrentCustomerPackageAsync([customer.Id]))
            .FirstOrDefault(cp => cp.Key == customer.Id).Value;

        return customerResponse;
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(CustomerQueryParameter queryParameter)
    {
        var customers = await Context.Customers
            .Include(c => c.IdentityUser)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        var customerResponse = Mapper.Map<IEnumerable<CustomerDto>>(customers);
        var customerCurrentPackages = await GetCurrentCustomerPackageAsync(customers.Select(c => c.Id).ToList());

        foreach (var customer in customerResponse)
        {
            if (customerCurrentPackages.TryGetValue(customer.Id, out var customerPackage))
            {
                customer.CustomerPackage = customerPackage;
            }
        }

        return customerResponse;
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
        await ValidateAsync(customerDeviceTokenDto);

        // find if the token already have a customer with it
        var customerWithToken = await Context.Customers
            .FirstOrDefaultAsync(c => c.DeviceToken == customerDeviceTokenDto.DeviceToken);
        
        // if so, delete that token of that customer
        if (customerWithToken != null)
        {
            customerWithToken.DeviceToken = null;
            Context.Customers.Update(customerWithToken);
        }

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
