namespace PillPal.Application.Features.Statistics;

public class StatisticRepository(IApplicationDbContext context)
    : BaseRepository(context), IStatisticService
{
    private static ReportTime HandleReportTime(ReportTime reportTime)
    {
        if (reportTime.StartDate == null || reportTime.EndDate == null)
        {
            reportTime.StartDate = DateTime.Now;
            reportTime.EndDate = DateTime.Now;
        }

        return reportTime;
    }

    public async Task<IEnumerable<CustomerPackagePercent>> GetCustomerPackagePercentAsync(ReportTime reportTime)
    {
        HandleReportTime(reportTime);

        var customerPackages = await Context.CustomerPackages
            .Include(cp => cp.PackageCategory)
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .AsNoTracking()
            .ToListAsync();

        //grouping by package category
        var groupByPackageCategory = customerPackages
            .GroupBy(cp => cp.PackageCategoryId)
            .Select(g => new CustomerPackagePercent
            {
                PackageName = g.First().PackageCategory!.PackageName,
                Percent = (double)g.Count() / customerPackages.Count * 100
            });

        return groupByPackageCategory;
    }

    public async Task<CustomerPackageReport> GetCustomerPackageReportAsync(ReportTime reportTime)
    {
        HandleReportTime(reportTime);

        var customerPackages = await Context.CustomerPackages
            .Include(cp => cp.PackageCategory)
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .AsNoTracking()
            .ToListAsync();

        var totalCustomerPackageSubcription = customerPackages.Count;

        return new CustomerPackageReport
        {
            TotalCustomerPackageSubcription = totalCustomerPackageSubcription,
            TotalRevenue = customerPackages.Sum(cp => cp.Price)
        };
    }

    public async Task<ReportByTime> GetReportsAsync(ReportTime reportTime)
    {
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
            Revenue = revenue
        };
    }

    public async Task<TopCustomerPackage?> GetTopCustomerPackageAsync(ReportTime reportTime)
    {
        HandleReportTime(reportTime);

        var topCustomerPackage = await Context.CustomerPackages
            .Where(cp => cp.StartDate >= reportTime.StartDate &&
                        cp.StartDate <= reportTime.EndDate)
            .GroupBy(cp => cp.PackageCategoryId)
            .Select(g => new TopCustomerPackage
            {
                PackageName = g.First().PackageCategory!.PackageName,
                TotalCustomer = g.Count(),
                TotalRevenue = g.Sum(cp => cp.Price)
            })
            .OrderByDescending(g => g.TotalRevenue)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return topCustomerPackage;
    }

    public async Task<CustomerRegistration> GetTotalCustomerRegistrationAsync(ReportTime reportTime)
    {
        HandleReportTime(reportTime);

        var totalCustomer = await Context.Customers
            .Where(c => c.CreatedAt!.Value >= reportTime.StartDate &&
                        c.CreatedAt!.Value <= reportTime.EndDate)
            .AsNoTracking()
            .CountAsync();

        return new CustomerRegistration
        {
            TotalCustomer = totalCustomer
        };
    }
}
