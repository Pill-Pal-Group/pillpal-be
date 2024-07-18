using PillPal.Application.Features.Statistics;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IStatisticService
{
    Task<CustomerRegistration> GetTotalCustomerRegistrationAsync(ReportTime reportTime);
    Task<TopCustomerPackage?> GetTopCustomerPackageAsync(ReportTime reportTime);
    Task<CustomerPackageReport> GetCustomerPackageReportAsync(ReportTime reportTime);
    Task<IEnumerable<CustomerPackagePercent>> GetCustomerPackagePercentAsync(ReportTime reportTime);

    //get report by time, could be 1 month, 3 months, 6 months, 1 year
    Task<ReportByTime> GetReportsAsync(ReportTime reportTime);
}
