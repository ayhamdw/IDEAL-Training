namespace DataEntity.Models;

public class ProductQueryModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "id";
}