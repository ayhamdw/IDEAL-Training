namespace DataEntity.ViewModels;

public class CreateCategoryViewModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentId { get; set; }
    public byte status { get; set; }
}