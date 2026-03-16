namespace BlazorGridApp.Client.Models
{
    public sealed record CsvColumnDefinition<T>(
        string Header,
        Func<T, string> GetValue);
}
