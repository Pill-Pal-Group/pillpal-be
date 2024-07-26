using PillPal.Application.Common.Interfaces.Payment;

namespace PillPal.Application.Features.Payments;

public class PaymentRepository(IApplicationDbContext context, IServiceProvider serviceProvider, IUser user,
    IZaloPayService zaloPayService, IVnPayService vnPayService)
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

        string paymentRef;
        Guid id;

        switch (packagePaymentInfo.PaymentType)
        {
            case PaymentEnums.ZALOPAY:
                paymentRef = DateTime.Now.ToString("yymmdd") + "_" + Guid.NewGuid().ToString();
                paymentRequest.PaymentReference = paymentRef;
                id = await CreatePendingCustomerPackageAsync(packageInformation, paymentRef);
                return new PaymentResponse
                {
                    PaymentUrl = zaloPayService.GetPaymentUrl(paymentRequest),
                    CustomerPackageId = id
                };
            case PaymentEnums.VNPAY:
                paymentRef = Guid.NewGuid().ToString();
                paymentRequest.PaymentReference = paymentRef;
                id = await CreatePendingCustomerPackageAsync(packageInformation, paymentRef);
                return new PaymentResponse
                {
                    PaymentUrl = vnPayService.GetPaymentUrl(paymentRequest),
                    CustomerPackageId = id
                };
            default:
                throw new BadRequestException("Invalid payment method.");
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

    private async Task<Guid> CreatePendingCustomerPackageAsync(PackageCategory packageCategory, string paymentRef)
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
            PaymentReference = paymentRef,
            PaymentStatus = (int)PaymentStatusEnums.UNPAID
        };

        await Context.CustomerPackages.AddAsync(customerPackage);

        await Context.SaveChangesAsync();

        return customerPackage.Id;
    }

    public async Task UpdatePaymentStatusAsync(string paymentRef, PaymentStatusEnums paymentStatus)
    {
        var customerPackage = await Context.CustomerPackages
            .FirstOrDefaultAsync(c => c.PaymentReference == paymentRef);

        if (customerPackage == null)
        {
            throw new NotFoundException(nameof(CustomerPackage), paymentRef);
        }

        customerPackage.PaymentStatus = (int)paymentStatus;

        await Context.SaveChangesAsync();
    }
}
