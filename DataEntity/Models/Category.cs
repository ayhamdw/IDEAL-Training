using DataEntity.ViewModels;
namespace DataEntity.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentId { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    private Category() {}

    public Category(CreateCategoryViewModel categoryViewModel)
    {
        Name = categoryViewModel.Name;
        Image = categoryViewModel.Image;
        ParentId = categoryViewModel.ParentId;
        Status = categoryViewModel.status;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}