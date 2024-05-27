using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Customers;

namespace PillPal.Application.Repositories;

public class CustomerRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateCustomerValidator>();

        var validationResult = await validator.ValidateAsync(createCustomerDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var customer = _mapper.Map<Customer>(createCustomerDto);

        await _context.Customers.AddAsync(customer);

        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
    {
        var customers = await _context.Customers
            .Include(c => c.IdentityUser)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto updateCustomerDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateCustomerValidator>();

        var validationResult = await validator.ValidateAsync(updateCustomerDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var customer = await _context.Customers
            .Include(c => c.IdentityUser)
            .FirstOrDefaultAsync(c => c.Id == customerId) ?? throw new NotFoundException(nameof(Customer), customerId);

        _mapper.Map(updateCustomerDto, customer);

        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }
}
