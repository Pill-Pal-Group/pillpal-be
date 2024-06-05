﻿namespace PillPal.Application.Features.Categories;

public record CategoryQueryParameter
{
    public string? CategoryCode { get; init; }
}

public static class CategoryQueryExtensions
{
    public static IQueryable<Category> Filter(this IQueryable<Category> query, CategoryQueryParameter queryParameter)
    {
        if (queryParameter.CategoryCode is not null)
        {
            query = query.Where(c => c.CategoryCode!.Contains(queryParameter.CategoryCode));
        }

        return query;
    }
}
