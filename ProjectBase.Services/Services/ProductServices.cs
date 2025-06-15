using System.Collections;
using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Services.Helpers;
using ProjectBase.Services.IServices;
using X.PagedList;

namespace ProjectBase.Services.Services;

public class ProductServices : IProductService
{
    private readonly ProjectBaseContext _context;

    public ProductServices(ProjectBaseContext context)
    {
        _context = context;
    }
    public CategoryProductsViewModel GetProducts(ProductQueryModel queryModel)
    {
        var products = _context.Product.AsEnumerable();
        
        var meta = BuildMeta(products, queryModel);
        products = CollectionUtils.ApplySorting(products, queryModel.SortBy);
        products = CollectionUtils.ApplyPagination(products, queryModel.PageNumber, queryModel.PageSize);
        
        var buildProductViewModel = BuildProduct(products);
        var categoryProductViewModel = BuildProductCategories(buildProductViewModel, meta);
        return categoryProductViewModel;
    }

    private IEnumerable<ProductViewModel> BuildProduct(IEnumerable<Product> products)
    {
        var brands = _context.Brands.ToList();
        var productCategories = _context.ProductCategory.ToList();
        var categories = _context.Category.ToList();

        return products.Select(product =>
        {
            var brand = brands.FirstOrDefault(b => b.Id == product.BrandId);
        
            var productCategoryIds = GetProductCategoriesIds(productCategories, product);

            var productCategoriesList = GetProductCategories(categories, productCategoryIds);

            var productViewModel = BuildProductViewModel(product, brand, productCategoriesList);
            
            return productViewModel;
        });
    }


    private MetaViewModel BuildMeta(IEnumerable<Product> products , ProductQueryModel queryModel)
    {
        var totalProducts = products.Count();
        var meta = new MetaViewModel(totalProducts , queryModel.PageNumber , queryModel.PageSize);
        return meta;
    }

    private IEnumerable<int> GetProductCategoriesIds(List<ProductCategory> productCategories , Product product)
    {
        var result = productCategories
            .Where(pc => pc.ProductId == product.Id)
            .Select(pc => pc.CategoryId);
        return result;
    }

    private List<CategoryViewModel> GetProductCategories(List<Category> categories ,IEnumerable<int> productCategoryIds)
    {
           var result =  categories
            .Where(c => productCategoryIds.Contains(c.Id))
            .Select(c => new CategoryViewModel(c.Id , c.Name))
            .ToList();
           return result;
    }

    private ProductViewModel BuildProductViewModel(Product product, Brand brand,
        List<CategoryViewModel> categories)
    {
        return new ProductViewModel(product, new BrandViewModel(brand), categories);
    }

    private CategoryProductsViewModel BuildProductCategories(IEnumerable<ProductViewModel> products,
        MetaViewModel meta)
    {
        return new CategoryProductsViewModel (products, meta);
    }

    public async Task<ProductViewModel?> CreateProduct(CreateProductViewModel createProductViewModel)
    {
        var brandId = createProductViewModel.BrandId;
        var check = await _context.Brands.AnyAsync(c => c.Id == brandId);
        if (!check) throw new ApplicationException("Brand does not exist");
        
        var product = new Product(createProductViewModel);
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();

        var categories = await createProductViewModel.CategoriesIds
            .Select(categoryId => new ProductCategory(product.Id , categoryId))
            .ToListAsync();
        
        await _context.ProductCategory.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        
        var result = BuildProduct([product]).SingleOrDefault();
        return result;
    } 
}