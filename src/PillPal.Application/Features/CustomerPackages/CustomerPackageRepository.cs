using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.CustomerPackages;

public class CustomerPackageRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), ICustomerPackageService
{
    public async Task<CustomerPackageDto> CreateCustomerPackageAsync(CreateCustomerPackageDto createCustomerPackageDto)
    {
        await ValidateAsync(createCustomerPackageDto);

        var existingCustomerPackage = await Context.CustomerPackages
            .AsNoTracking()
            .FirstOrDefaultAsync(c => !c.IsExpired)
            ?? throw new BadRequestException("Customer already has an active package.");

        //todo: need improvement
        var package = await Context.PackageCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == createCustomerPackageDto.PackageId)
            ?? throw new NotFoundException(nameof(PackageCategories), createCustomerPackageDto.PackageId);

        var payment = await Context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == createCustomerPackageDto.PaymentId)
            ?? throw new NotFoundException(nameof(Payment), createCustomerPackageDto.PaymentId);

        var customerId = await Context.Customers
            .AsNoTracking()
            .Where(c => c.IdentityUserId == Guid.Parse(user.Id!))
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        var customerPackage = Mapper.Map<CustomerPackage>(createCustomerPackageDto);

        customerPackage.Duration = package.PackageDuration;
        customerPackage.StartDate = DateTimeOffset.UtcNow;
        customerPackage.EndDate = DateTimeOffset.UtcNow.AddDays(package.PackageDuration);
        customerPackage.Price = package.Price;
        customerPackage.CustomerId = customerId;
        customerPackage.PackageId = package.Id;
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
