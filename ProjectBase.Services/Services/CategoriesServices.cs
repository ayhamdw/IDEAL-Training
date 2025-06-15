using System.Linq.Dynamic.Core;
using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Core;
using ProjectBase.Services.IServices;
using ProjectBase.Services.Helpers;
using CollectionUtils = ProjectBase.Services.Helpers.CollectionUtils;

namespace ProjectBase.Services.Services;

public class CategoriesServices : ICategoriesService
{
    
    private readonly ProjectBaseContext _context;

    public CategoriesServices(ProjectBaseContext context)
    {
        _context = context;
    }
public async Task<CategoryProductsViewModel> GetCategoryProductsById(int id, CategoryQueryModel queryModel)
{
    var category = await GetCategoryViewModelById(id);
    if (category == null) return null;

    var allCategoryIds = await GetAllSubcategoryIds(id);
    var productsQuery = BuildProductsQuery(allCategoryIds, queryModel);

    var products = await productsQuery;
    var totalProducts = await CountDistinctProductsInCategories(allCategoryIds);

    var meta = CreateMetaModel(totalProducts, queryModel);
    return new CategoryProductsViewModel(products, meta, category);
}

private async Task<CategoryViewModel?> GetCategoryViewModelById(int id)
{
    return await _context.Category
        .Where(c => c.Id == id)
        .Select(c => new CategoryViewModel(c, _context.ProductCategory.Count(pc => pc.CategoryId == c.Id)))
        .FirstOrDefaultAsync();
}

private async Task<IEnumerable<ProductViewModel>> BuildProductsQuery(IEnumerable<int> categoryIds, CategoryQueryModel queryModel)
{

    var query =  _context.ProductCategory
        .Where(pc => categoryIds.Contains(pc.CategoryId))
        .Join(_context.Product,
            pc => pc.ProductId,
            p => p.Id,
            (pc, p) => p)
        .Join(_context.Brands,
            p => p.BrandId,
            b => b.Id,
            (p, b) => new { Product = p, Brand = b})
        .Distinct()
        .Select(x => new ProductViewModel(x.Product,  new BrandViewModel(x.Brand) , new List <CategoryViewModel> () ))
        .AsEnumerable();
    
   query = CollectionUtils.ApplySorting(query , queryModel.SortBy);
   query = CollectionUtils.ApplyPagination(query, queryModel.Page , queryModel.PageSize);
   return query;
}
private async Task<int> CountDistinctProductsInCategories(IEnumerable<int> categoryIds)
{
    return await _context.ProductCategory
        .Where(pc => categoryIds.Contains(pc.CategoryId))
        .Select(pc => pc.ProductId)
        .Distinct()
        .CountAsync();
}

private MetaViewModel CreateMetaModel(int totalItems, CategoryQueryModel queryModel)
{
    return new MetaViewModel (totalItems ,queryModel.Page ,queryModel.PageSize);
}

    public async Task<CategoryViewModel> AddNewCategory(CreateCategoryViewModel createCategoryViewModel)
    {

        if (createCategoryViewModel.ParentId.HasValue)
        {
            var parentExists = await _context.Category.AnyAsync(e => e.ParentId == createCategoryViewModel.ParentId);
            if (!parentExists) throw new ArgumentException($"Parent with id = {createCategoryViewModel.ParentId} not found!");
        }

        var category = new Category(createCategoryViewModel);
        await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync();

        var result = new CategoryViewModel(category, 0);
        return result;
    }
    private async Task<HashSet<int>> GetAllSubcategoryIds(int categoryId , int page = 1, int perPage = 10)
{
    var result = new HashSet<int> { categoryId };
    
    var allCategories = await _context.Category.ToListAsync();
    
    var queue = new Queue<int>();
    queue.Enqueue(categoryId);
    
    while (queue.Count > 0)
    {
        var currentId = queue.Dequeue();
        
        var childCategories = allCategories
            .Where(c => c.ParentId.HasValue && c.ParentId.Value == currentId)
            .ToList();
        
        foreach (var child in childCategories)
        {
            if (result.Add(child.Id)) 
            {
                queue.Enqueue(child.Id);
            }
        }
    }
    
    return result;
}
    public async Task<List<CategoryViewModel>> GetAllCategories()
    {
        var allCategories = await _context.Category.ToListAsync();
        var allProducts = await _context.Product.ToListAsync();
        var result = GetAllCategoriesWithSubcategories(allCategories , allProducts);
        return result;
    }

    private static List<CategoryViewModel> GetAllCategoriesWithSubcategories(List<Category> allCategories , List<Product> allProducts)
    {
        var categoryVMs = allCategories.Select(c => new CategoryViewModel (c , allProducts.Count(p => p.Id == c.Id))).ToList();

        var categoryDict = categoryVMs.ToDictionary(c => c.Id);

        List<CategoryViewModel> rootCategories = [];

        foreach (var category in allCategories)
        {
            if (category.ParentId == null)
            {
                rootCategories.Add(categoryDict[category.Id]);
            }
            else if (categoryDict.TryGetValue(category.ParentId.Value, out var parent))
            {
                parent.SubCategories.Add(categoryDict[category.Id]);
                parent.ProductNumber += categoryDict[category.Id].ProductNumber;
            }
        }
        return rootCategories;
    }
    
}