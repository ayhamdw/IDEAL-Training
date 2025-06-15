namespace DataEntity.Models;

public class ProductCategory
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    
    public ProductCategory() {}

    public ProductCategory(int productId, int categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }
}
