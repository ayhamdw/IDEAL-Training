using DataEntity.Models;

namespace DataEntity.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int SalePercentage { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal CurrentPrice { get; private set; } 
    public bool IsOnSale { get; private set; }
    public BrandViewModel Brand { get; set; }

    public List<CategoryViewModel> Categories { get; set; } = [];
    public ProductViewModel(Product product, BrandViewModel brand , List <CategoryViewModel> categories )
    {
        ArgumentNullException.ThrowIfNull(product);

        Id = product.Id;
        Name = product.Name;
        OriginalPrice = product.OriginalPrice;
        SalePercentage = product.SalePercentage;
        ImageUrl = product.Image;
        IsOnSale = SalePercentage > 0;
        CurrentPrice = IsOnSale 
            ? OriginalPrice - (OriginalPrice * SalePercentage / 100)
            : OriginalPrice;
        Brand = brand;
        Categories = categories;
    }
}