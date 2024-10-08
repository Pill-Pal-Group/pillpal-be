﻿using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IMedicineService
{
    /// <summary>
    /// Retrieves all medicines.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <param name="includeParameter">The include parameters for related entities.</param>
    /// <returns>
    /// The task result contains a pagination collection of <see cref="MedicineDto"/> objects.
    /// </returns>
    Task<PaginationResponse<MedicineDto>> GetMedicinesAsync(
        MedicineQueryParameter queryParameter, MedicineIncludeParameter includeParameter);

    /// <summary>
    /// Retrieves a medicine by its unique identifier.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine.</param>
    /// <returns>
    /// The task result contains the <see cref="MedicineDto"/> representing the found medicine.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<MedicineDto> GetMedicineByIdAsync(Guid medicineId);

    /// <summary>
    /// Creates a new medicine.
    /// </summary>
    /// <param name="createMedicineDto">The DTO containing the creation data for the medicine.</param>
    /// <returns>
    /// The task result contains the created <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto createMedicineDto);

    /// <summary>
    /// Creates a new medicine with full of its related entities.
    /// </summary>
    /// <param name="createFullMedicineDto">The DTO containing the creation data for the medicine.</param>
    /// <returns>
    /// The task result contains the created <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<MedicineDto> CreateFullMedicineAsync(CreateFullMedicineDto createFullMedicineDto);

    /// <summary>
    /// Creates batch of medicines from excel file import.
    /// Other entities related to the medicine will be created if not exist.
    /// </summary>
    /// <param name="excelMedicineListToInsert">The list of DTOs containing the creation data for the medicines.</param>
    /// <returns>
    /// The task result contains the number of rows affected.
    /// </returns>
    Task<int> CreateMedicinesFromExcelBatchAsync(List<CreateMedicineFromExcelDto> excelMedicineListToInsert);

    /// <summary>
    /// Retrieves currently available price units for medicines.
    /// </summary>
    /// <returns>
    /// The task result contains the <see cref="MedicinePriceUnitsDto"/> representing the list of available price units.
    /// </returns>
    Task<MedicinePriceUnitsDto> GetMedicinePriceUnitsAsync();

    /// <summary>
    /// Adds brand to medicine with price.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine.</param>
    /// <param name="createMedicineInBrandDto">The DTO containing the creation data for the medicine in brand.</param>
    /// <exception cref="NotFoundException">Thrown if the either the medicine or brand is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task CreateMedicineInBrandAsync(Guid medicineId, CreateMedicineInBrandsDto createMedicineInBrandDto);

    /// <summary>
    /// Updates an existing medicine in brand.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine.</param>
    /// <param name="updateMedicineInBrandDto">The DTO containing update information for the medicine in brand.</param>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>    
    Task UpdateMedicineInBrandAsync(Guid medicineId, UpdateMedicineInBrandsDto updateMedicineInBrandDto);

    /// <summary>
    /// Updates an existing medicine.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine to update.</param>
    /// <param name="updateMedicineDto">The DTO containing update information for the medicine.</param>
    /// <returns>
    /// The task result contains the updated <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto);

    /// <summary>
    /// Updates an existing medicine with full of its related entities.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine to update.</param>
    /// <param name="updateFullMedicineDto">The DTO containing update information for the medicine.</param>
    /// <returns>
    /// The task result contains the updated <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<MedicineDto> UpdateFullMedicineAsync(Guid medicineId, UpdateFullMedicineDto updateFullMedicineDto);

    /// <summary>
    /// Deletes a medicine by performing a soft delete.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteMedicineAsync(Guid medicineId);

    /// <summary>
    /// Deletes a medicine in brand by performing a soft delete.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine.</param>
    /// <param name="brandId">The unique identifier for the brand.</param>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteMedicineInBrandAsync(Guid medicineId, Guid brandId);

    /// <summary>
    /// Imports medicines from excel file.
    /// </summary>
    /// <param name="file">The excel file to import.</param>
    /// <param name="excelProperties">The properties to map the excel columns to the medicine properties.</param>
    /// <param name="excelPropertyDelimiters">The delimiter for properties with multiple values.</param>
    /// <returns>
    /// The task result contains the <see cref="FileExecutionResult"/> representing the import result.
    /// </returns>
    Task<FileExecutionResult> ImportMedicinesAsync(Stream file, MedicineExcelProperties excelProperties, ExcelPropertyDelimiters excelPropertyDelimiters);
}
