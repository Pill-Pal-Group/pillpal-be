﻿using PillPal.Application.Features.Customers;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    /// <summary>
    /// Gets a customer by its unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier for the customer.</param>
    /// <returns>
    /// The task result contains the <see cref="CustomerDto"/> representing the found customer.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);

    /// <summary>
    /// Gets the customer information.
    /// </summary>
    /// <returns>
    /// The task result contains the <see cref="CustomerDto"/> representing the customer information.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<CustomerDto> GetCustomerInfoAsync();

    /// <summary>
    /// Updates an existing customer information.
    /// </summary>
    /// <param name="updateCustomerDto">The DTO containing update information for the customer.</param>
    /// <returns>
    /// The task result contains the updated <see cref="CustomerDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <param name="queryParameter">The query parameter to filter the customers.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="CustomerDto"/> objects.
    /// </returns>
    Task<PaginationResponse<CustomerDto>> GetCustomersAsync(CustomerQueryParameter queryParameter);

    /// <summary>
    /// Updates the meal time information for a customer.
    /// </summary>
    /// <param name="updateCustomerMealTimeDto">The DTO containing update information for the meal time.</param>
    /// <returns>
    /// The task result contains the updated <see cref="CustomerMealTimeDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<CustomerMealTimeDto> UpdateCustomerMealTimeAsync(UpdateCustomerMealTimeDto updateCustomerMealTimeDto);

    /// <summary>
    /// Updates the device token for a customer.
    /// </summary>
    /// <param name="customerDeviceTokenDto">The DTO containing the device token information.</param>
    /// <returns>
    /// The task result contains the updated <see cref="CustomerDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task UpdateCustomerDeviceTokenAsync(CustomerDeviceTokenDto customerDeviceTokenDto);
}
