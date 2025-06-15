using DataEntity.Models;

namespace DataEntity.ViewModels;

public class BrandViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }

    public BrandViewModel(Brand brand)
    {
        this.Id = brand.Id;
        this.Name = brand.Name;
        this.Logo = brand.Logo;
    }

}