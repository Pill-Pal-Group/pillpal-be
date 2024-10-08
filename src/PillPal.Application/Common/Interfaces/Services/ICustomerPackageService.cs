﻿using PillPal.Application.Features.CustomerPackages;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ICustomerPackageService
{
    /// <summary>
    /// Get all customer packages
    /// </summary>
    /// <param name="isCustomer">
    /// If true, return only customer packages, default value is false
    /// </param>
    /// <returns>
    /// The task result contains a collection of <see cref="CustomerPackageDto"/> objects.
    /// </returns>
    Task<IEnumerable<CustomerPackageDto>> GetCustomerPackagesAsync(bool isCustomer = false);

    /// <summary>
    /// Retrieves a customer package by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the customer package.</param>
    /// <param name="isCustomer">
    /// If true, return only customer package, default value is false
    /// </param>
    /// <returns>
    /// The task result contains the <see cref="CustomerPackageDto"/> representing the found customer package.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<CustomerPackageDto> GetCustomerPackageAsync(Guid id, bool isCustomer = false);

    /// <summary>
    /// Checks for expired packages and updates their status.
    /// This method should be called periodically by a background service.
    /// </summary>
    /// <returns>The task result.</returns>
    Task CheckForExpiredPackagesAsync();

    /// <summary>
    /// To update payment to paid status
    /// </summary>
    /// <param name="customerPackageId"></param>
    /// <returns>The task result.</returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task UpdateConfirmPackagePayment(Guid customerPackageId);

    /// <summary>
    /// Checks for packages that are about to expire and sends a notification to the customer.
    /// This method should be called periodically by a background service.
    /// </summary>
    /// <returns>The task result.</returns>
    Task CheckForRenewPackage();

    /// <summary>
    /// Cleans up the database by removing all unpaid packages.
    /// Packages subjected to removal are those that have been unpaid for more than 24 hours.
    /// This method should be called periodically by a background service.
    /// </summary>
    /// <returns>The task result.</returns>
    Task RemoveUnpaidPackagesAsync();
}
