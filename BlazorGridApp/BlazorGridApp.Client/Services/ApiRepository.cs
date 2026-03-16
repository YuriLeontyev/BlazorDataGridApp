using BlazorGridApp.Client.Models;
using System.Globalization;
using System.Net.Http.Json;

namespace BlazorGridApp.Client.Services;

public class ApiRepository<T>(HttpClient http)
{
    public async Task<(List<T> Items, int Count)> GetManyAsync(GridQueryState state, bool applyPaging = true, CancellationToken cancellationToken = default)
    {
        var url = $"api/products{ToODataQueryString(state, applyPaging)}";

        var result = await http.GetFromJsonAsync<PagedResult<T>>(url, cancellationToken);
        return (result?.Items ?? [], result?.TotalCount ?? 0);
    }

    protected string ToODataQueryString(GridQueryState state, bool applyPaging = true)
    {
        var parts = new List<string>();

        var filterParts = state.Filters
            .Select(ToODataExpression)
            .OfType<string>()
            .ToList();

        if (filterParts.Count > 0)
            parts.Add($"$filter={Uri.EscapeDataString(string.Join(" and ", filterParts))}");

        if (!string.IsNullOrWhiteSpace(state.Sort.Field))
            parts.Add($"$orderby={Uri.EscapeDataString($"{state.Sort.Field} {(state.Sort.Descending ? "desc" : "asc")}")}");

        if (applyPaging)
        {
            parts.Add($"$skip={state.Skip}");
            parts.Add($"$top={state.Top}");
            parts.Add("$count=true");
        }
        else
        {
            parts.Add($"$top={int.MaxValue}");
        }

        return "?" + string.Join("&", parts);
    }

    private static string? ToODataExpression(FilterDescriptor f) =>
        f.Value is null ? null : f.Operator switch
        {
            FilterOperator.Contains when f.Value is string s => $"contains({f.Field},'{Escape(s)}')",
            FilterOperator.StartsWith when f.Value is string s => $"startswith({f.Field},'{Escape(s)}')",
            FilterOperator.Equals when f.Value is bool b => $"{f.Field} eq {b.ToString().ToLowerInvariant()}",
            FilterOperator.Equals when f.Value is string s => $"{f.Field} eq '{Escape(s)}'",
            FilterOperator.GreaterThan => $"{f.Field} ge {FormatValue(f.Value)}",
            FilterOperator.LessThan => $"{f.Field} le {FormatValue(f.Value)}",
            _ => null
        };

    private static string Escape(string s) => s.Replace("'", "''");

    private static string FormatValue(object value) => value switch
    {
        decimal d => d.ToString(CultureInfo.InvariantCulture),
        double d => d.ToString(CultureInfo.InvariantCulture),
        float f => f.ToString(CultureInfo.InvariantCulture),
        int i => i.ToString(CultureInfo.InvariantCulture),
        DateOnly d => d.ToString("yyyy-MM-dd"),
        DateTime d => DateOnly.FromDateTime(d).ToString("yyyy-MM-dd"),
        _ => value.ToString() ?? string.Empty
    };
}
