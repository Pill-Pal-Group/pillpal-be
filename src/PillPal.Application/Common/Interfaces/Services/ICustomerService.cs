using PillPal.Application.Features.Customers;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<CustomerDto> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto updateCustomerDto);
    Task<IEnumerable<CustomerDto>> GetCustomersAsync();
}
