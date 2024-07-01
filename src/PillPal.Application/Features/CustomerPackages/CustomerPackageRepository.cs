using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.CustomerPackages;

public class CustomerPackageRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), ICustomerPackageService
{
    public async Task CheckForExpiredPackagesAsync()
    {
        var expiredPackages = await Context.CustomerPackages
            .Where(c => c.EndDate < DateTimeOffset.UtcNow)
            .ToListAsync();

        foreach (var expiredPackage in expiredPackages)
        {
            expiredPackage.IsExpired = true;
        }

        await Context.SaveChangesAsync();
    }

    public async Task<CustomerPackageDto> CreateCustomerPackageAsync(CreateCustomerPackageDto createCustomerPackageDto)
    {
        await ValidateAsync(createCustomerPackageDto);

        var customerId = await Context.Customers
            .AsNoTracking()
            .Where(c => c.IdentityUserId == Guid.Parse(user.Id!))
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        var existingCustomerPackage = await Context.CustomerPackages
            .AsNoTracking()
            .Where(c => c.CustomerId == customerId)
            .FirstOrDefaultAsync(c => !c.IsExpired);
        
        if (existingCustomerPackage != null)
        {
            throw new BadRequestException("Customer already has an active package.");
        }

        //todo: need improvement
        var package = await Context.PackageCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == createCustomerPackageDto.PackageCategoryId)
            ?? throw new NotFoundException(nameof(PackageCategories), createCustomerPackageDto.PackageCategoryId);

        var payment = await Context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == createCustomerPackageDto.PaymentId)
            ?? throw new NotFoundException(nameof(Payment), createCustomerPackageDto.PaymentId);

        var customerPackage = Mapper.Map<CustomerPackage>(createCustomerPackageDto);

        customerPackage.Duration = package.PackageDuration;
        customerPackage.StartDate = DateTimeOffset.UtcNow;
        customerPackage.EndDate = DateTimeOffset.UtcNow.AddDays(package.PackageDuration);
        customerPackage.Price = package.Price;
        customerPackage.CustomerId = customerId;
        customerPackage.PackageCategoryId = package.Id;
        customerPackage.PaymentId = payment.Id;

        await Context.CustomerPackages.AddAsync(customerPackage);

        await Context.SaveChangesAsync();

        return Mapper.Map<CustomerPackageDto>(customerPackage);
    }

    public async Task<CustomerPackageDto> GetCustomerPackageAsync(Guid id, bool isCustomer)
    {
        var customerPackageQueryable = Context.CustomerPackages
            .Include(c => c.Customer)
            .AsNoTracking();

        if (isCustomer)
        {
            customerPackageQueryable = customerPackageQueryable
                .Where(c => c.Customer!.IdentityUserId == Guid.Parse(user.Id!));
        }

        var customerPackage = await customerPackageQueryable
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException(nameof(CustomerPackage), id);
        
        return Mapper.Map<CustomerPackageDto>(customerPackage);
    }

    public async Task<IEnumerable<CustomerPackageDto>> GetCustomerPackagesAsync(bool isCustomer)
    {
        var customerPackagesQueryable = Context.CustomerPackages
            .Include(c => c.Customer)
            .AsNoTracking();

        if (isCustomer)
        {
            customerPackagesQueryable = customerPackagesQueryable
                .Where(c => c.Customer!.IdentityUserId == Guid.Parse(user.Id!));
        }

        var customerPackages = await customerPackagesQueryable
            .ToListAsync();

        return Mapper.Map<IEnumerable<CustomerPackageDto>>(customerPackages);
    }
}
