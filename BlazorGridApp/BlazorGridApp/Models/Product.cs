namespace BlazorGridApp.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
