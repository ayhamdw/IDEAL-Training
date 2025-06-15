namespace DataEntity.ViewModels;

public class CreateProductViewModel
{
    public string Name { get; set; }
    public int BrandId { get; set; }
    public decimal OriginalPrice { get; set; }
    public int SalePercentage { get; set; }
    public List<int> CategoriesIds { get; set; }
    public string CreatedBy { get; set; }
}