namespace BlazorGridApp.Client.Models;

public enum FilterOperator { Equals, Contains, GreaterThan, LessThan, StartsWith }

public enum FilterValueType { Text, Number, Date, Boolean }

public record FilterDescriptor(string Field, FilterOperator Operator, object? Value);

public record SortDescriptor(string Field = "", bool Descending = false);

public record FilterFieldDefinition(
    string Field,
    string Label,
    FilterOperator Operator,
    FilterValueType ValueType
);
