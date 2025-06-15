namespace DataEntity.Models;

public class CategoryQueryModel
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
}