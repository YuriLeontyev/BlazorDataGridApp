using System.Globalization;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorGridApp.Client.Models;

public record GridQueryState
{
    public int Skip { get; init; }
    public int Top { get; init; } = 10;
    public SortDescriptor Sort { get; init; } = new();
    public List<FilterDescriptor> Filters { get; init; } = [];

    public static GridQueryState FromRequest<T>(
        GridItemsProviderRequest<T> request,
        int defaultPageSize,
        List<FilterDescriptor> filters)
    {
        var sortProperties = request.GetSortByProperties();
        var hasSort        = sortProperties.Count > 0;
        var sortProperty   = hasSort ? sortProperties.First() : default;

        return new GridQueryState
        {
            Skip    = request.StartIndex,
            Top     = request.Count ?? defaultPageSize,
            Sort    = hasSort
                        ? new SortDescriptor(sortProperty.PropertyName, sortProperty.Direction == SortDirection.Descending)
                        : new SortDescriptor(),
            Filters = filters,
        };
    }   
}

