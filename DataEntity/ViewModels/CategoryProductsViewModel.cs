namespace DataEntity.ViewModels;

public class CategoryProductsViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; }
    public MetaViewModel Meta { get; set; }
    public CategoryViewModel Category { get; set; }

    public CategoryProductsViewModel(IEnumerable<ProductViewModel> products, MetaViewModel meta)
    {
        Products = products;
        Meta = meta;
    }
    public CategoryProductsViewModel(IEnumerable<ProductViewModel> products, MetaViewModel meta, CategoryViewModel category)
    {
        Products = products;
        Meta = meta;
        Category = category;
    }
}

