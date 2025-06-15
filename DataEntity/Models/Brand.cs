namespace DataEntity.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}