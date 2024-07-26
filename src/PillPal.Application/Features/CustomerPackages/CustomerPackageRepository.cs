namespace PillPal.Application.Features.CustomerPackages;

public class CustomerPackageRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), ICustomerPackageService
{
    public async Task CheckForExpiredPackagesAsync()
    {
        var expiredPackages = await Context.CustomerPackages
            .Where(c => c.EndDate < DateTimeOffset.UtcNow)
            .Where(c => c.PaymentStatus == (int)PaymentStatusEnums.PAID)
            .ToListAsync();

        foreach (var expiredPackage in expiredPackages)
        {
            expiredPackage.IsExpired = true;
        }

        await Context.SaveChangesAsync();
    }

    public async Task<CustomerPackageDto> GetCustomerPackageAsync(Guid id, bool isCustomer)
    {
        var customerPackageQueryable = Context.CustomerPackages
            .Include(c => c.Customer)
            .AsNoTracking();

        if (isCustomer)
        {
            customerPackageQueryable = customerPackageQueryable
                .Where(c => c.PaymentStatus == (int)PaymentStatusEnums.PAID)
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
                .Where(c => c.PaymentStatus == (int)PaymentStatusEnums.PAID)
                .Where(c => c.Customer!.IdentityUserId == Guid.Parse(user.Id!));
        }

        var customerPackages = await customerPackagesQueryable
            .ToListAsync();

        return Mapper.Map<IEnumerable<CustomerPackageDto>>(customerPackages);
    }

    public async Task UpdateConfirmPackagePayment(Guid customerPackageId)
    {
        var customerPackage = await Context.CustomerPackages
            .FirstOrDefaultAsync(c => c.Id == customerPackageId)
            ?? throw new NotFoundException(nameof(CustomerPackage), customerPackageId);

        customerPackage.PaymentStatus = (int)PaymentStatusEnums.PAID;

        Context.CustomerPackages.Update(customerPackage);

        await Context.SaveChangesAsync();
    }
}
