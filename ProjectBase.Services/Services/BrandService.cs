using DataEntity.Models;
using DataEntity.ViewModels;
using ProjectBase.Services.IServices;
using X.PagedList;

namespace ProjectBase.Services.Services;
public class BrandService : IBrandService
{
    private readonly ProjectBaseContext _context;

    public BrandService(ProjectBaseContext context)
    {
        _context = context;
    }
    public async Task <List<BrandViewModel>> GetAllBrands(BrandQueryModel queryModel)
    {
        var status = queryModel.Active ? 1 : 0;
        var limit = queryModel.Limit > 0 ? queryModel.Limit : 10;
        var brands = _context.Brands
            .Where(e => e.Status == status)
            .Take(limit)
            .ToList();
            
        var brandsResponse = await brands.Select(e => new BrandViewModel(e)).ToListAsync();
        return brandsResponse;
    }
}