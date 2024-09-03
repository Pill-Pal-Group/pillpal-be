namespace PillPal.Application.Features.Statistics;

public class StatisticRepository(IApplicationDbContext context, IServiceProvider serviceProvider)
    : BaseRepository(context, serviceProvider), IStatisticService
{
    /// <summary>
    /// Incase the start date or end date is null, set them to current date
    /// </summary>
    private static ReportTimeRequest HandleReportTime(ReportTimeRequest reportTime)
    {
        if (reportTime.StartDate == null || reportTime.EndDate == null)
        {
            reportTime.StartDate = DateTime.Now;
            reportTime.EndDate = DateTime.Now;
        }

        return reportTime;
    }

    public async Task<IEnumerable<CustomerPackagePercentReport>> GetCustomerPackagePercentAsync(ReportTimeRequest reportTime)
    {
        await ValidateAsync(reportTime);
        HandleReportTime(reportTime);

        var customerPackages = await Context.CustomerPackages
            .Include(cp => cp.PackageCategory)
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .Where(cp => cp.PaymentStatus == (int)PaymentStatusEnums.PAID)
            .AsNoTracking()
            .ToListAsync();

        //grouping by package category
        var groupByPackageCategory = customerPackages
            .GroupBy(cp => cp.PackageCategoryId)
            .Select(g => new CustomerPackagePercentReport
            {
                PackageName = g.First().PackageCategory!.PackageName,
                Percent = (double)g.Count() / customerPackages.Count * 100,
                TotalRevenue = g.Sum(cp => cp.Price)
            });

        return groupByPackageCategory;
    }

    public async Task<CustomerPackageReport> GetCustomerPackageReportAsync(ReportTimeRequest reportTime)
    {
        await ValidateAsync(reportTime);
        HandleReportTime(reportTime);

        var customerPackages = await Context.CustomerPackages
            .Include(cp => cp.PackageCategory)
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .Where(cp => cp.PaymentStatus == (int)PaymentStatusEnums.PAID)
            .AsNoTracking()
            .ToListAsync();

        var totalCustomerPackageSubcription = customerPackages.Count;

        return new CustomerPackageReport
        {
            TotalCustomerPackageSubcription = totalCustomerPackageSubcription,
            TotalRevenue = customerPackages.Sum(cp => cp.Price)
        };
    }

    public async Task<ReportByTime> GetReportsAsync(ReportTimeRequest reportTime)
    {
        await ValidateAsync(reportTime);
        HandleReportTime(reportTime);

        var customerPackagePercents = await GetCustomerPackagePercentAsync(reportTime);
        var totalCustomerRegistration = await GetTotalCustomerRegistrationAsync(reportTime);
        var topCustomerPackage = await GetTopCustomerPackageAsync(reportTime);

        var packageReport = await GetCustomerPackageReportAsync(reportTime);

        var revenue = packageReport.TotalRevenue;
        var totalCustomerPackageSubcription = packageReport.TotalCustomerPackageSubcription;

        return new ReportByTime
        {
            CustomerPackagePercents = customerPackagePercents,
            TotalCustomerRegistration = totalCustomerRegistration.TotalCustomer,
            TotalCustomerPackageSubcription = totalCustomerPackageSubcription,
            TopPackage = topCustomerPackage,
            ReportTime = reportTime,
            TotalRevenue = revenue
        };
    }

    public async Task<TopCustomerPackageReport?> GetTopCustomerPackageAsync(ReportTimeRequest reportTime)
    {
        await ValidateAsync(reportTime);
        HandleReportTime(reportTime);

        var topCustomerPackage = await Context.CustomerPackages
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .Where(cp => cp.PaymentStatus == (int)PaymentStatusEnums.PAID)
            .GroupBy(cp => cp.PackageCategoryId)
            .Select(g => new TopCustomerPackageReport
            {
                PackageName = g.First().PackageCategory!.PackageName,
                TotalCustomer = g.Count(),
                TotalRevenue = g.Sum(cp => cp.Price)
            })
            .OrderByDescending(g => g.TotalCustomer)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return topCustomerPackage;
    }

    public async Task<CustomerRegistrationReport> GetTotalCustomerRegistrationAsync(ReportTimeRequest reportTime)
    {
        await ValidateAsync(reportTime);
        HandleReportTime(reportTime);

        var totalCustomer = await Context.Customers
            .Where(c => c.CreatedAt!.Value >= reportTime.StartDate &&
                        c.CreatedAt!.Value <= reportTime.EndDate)
            .AsNoTracking()
            .CountAsync();

        return new CustomerRegistrationReport
        {
            TotalCustomer = totalCustomer
        };
    }
}
