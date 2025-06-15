using DataEntity.Models;

namespace DataEntity.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ProductNumber { get; set; }
    public int? ParentId { get; set; }
    public List<CategoryViewModel> SubCategories { get; set; }


    public CategoryViewModel(int id , string name)
    {
        Id = id;
        Name = name;
    }
    public CategoryViewModel(Category category, int productNumber)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        Id = category.Id;
        Name = category.Name;
        Image = category.Image;
        ParentId = category.ParentId;
        ProductNumber = productNumber;
        SubCategories = new List<CategoryViewModel>();
    }
}