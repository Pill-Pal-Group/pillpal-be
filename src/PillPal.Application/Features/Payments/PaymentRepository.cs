using PillPal.Application.Common.Interfaces.Payment;

namespace PillPal.Application.Features.Payments;

public class PaymentRepository(IApplicationDbContext context, IServiceProvider serviceProvider, IUser user,
    IZaloPayService zaloPayService)
    : BaseRepository(context, serviceProvider), IPaymentService
{
    public async Task<PaymentResponse> CreatePaymentRequestAsync(CustomerPackagePaymentInformation packagePaymentInfo)
    {
        await ValidateAsync(packagePaymentInfo);

        var packageInformation = await GetPackageInformation(packagePaymentInfo.PackageCategoryId);

        var paymentRequest = new PaymentRequest
        {
            Amount = packageInformation.Price,
            Description = "Thanh toán gói " + packageInformation.PackageName,
        };

        Guid pendingCustomerPackageId;

        switch (packagePaymentInfo.PaymentType)
        {
            case PaymentEnums.ZALOPAY:
                pendingCustomerPackageId = await CreatePendingCustomerPackageAsync(packageInformation);
                (string paymentUrl, string zpTransToken) = zaloPayService.GetPaymentUrl(paymentRequest);
                return new PaymentResponse
                {
                    PaymentUrl = paymentUrl,
                    CustomerPackageId = pendingCustomerPackageId,
                    ZpTransToken = zpTransToken
                };
            default:
                throw new BadRequestException("Invalid or unsupported payment method.");
        }
    }

    private async Task<PackageCategory> GetPackageInformation(Guid packageCategoryId)
    {
        var package = await Context.PackageCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == packageCategoryId)
            ?? throw new NotFoundException(nameof(PackageCategories), packageCategoryId);

        return package;
    }

    private async Task<Guid> CreatePendingCustomerPackageAsync(PackageCategory packageCategory)
    {
        var customerId = await Context.Customers
            .AsNoTracking()
            .Where(c => c.IdentityUserId == Guid.Parse(user.Id!))
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        var existingCustomerPackage = await Context.CustomerPackages
            .AsNoTracking()
            .Where(c => c.CustomerId == customerId)
            .Where(c => c.PaymentStatus == (int)PaymentStatusEnums.PAID)
            .FirstOrDefaultAsync(c => !c.IsExpired);

        if (existingCustomerPackage != null)
        {
            throw new BadRequestException("Customer already has an active package.");
        }

        var customerPackage = new CustomerPackage
        {
            Duration = packageCategory.PackageDuration,
            StartDate = DateTimeOffset.UtcNow,
            EndDate = DateTimeOffset.UtcNow.AddDays(packageCategory.PackageDuration),
            Price = packageCategory.Price,
            CustomerId = customerId,
            PackageCategoryId = packageCategory.Id,
            PaymentStatus = (int)PaymentStatusEnums.UNPAID
        };

        await Context.CustomerPackages.AddAsync(customerPackage);

        await Context.SaveChangesAsync();

        return customerPackage.Id;
    }
}
