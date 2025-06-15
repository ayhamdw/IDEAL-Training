using DataEntity.ViewModels;

namespace DataEntity.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
    public decimal OriginalPrice { get; set; }
    public int SalePercentage { get; set; }
    public byte Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Image { get; set; }
    
    public Product () {}

    public Product(CreateProductViewModel model)
    {
        Name = model.Name;
        BrandId = model.BrandId;
        OriginalPrice = model.OriginalPrice;
        SalePercentage = model.SalePercentage;
        CreatedBy  = model.CreatedBy;
        CreatedAt = DateTime.Now;
    }
}
