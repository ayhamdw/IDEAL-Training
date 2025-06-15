using DataEntity.Models;
using DataEntity.ViewModels;

namespace ProjectBase.Services.IServices
{
    public interface IBrandService
    {
       Task <List<BrandViewModel>> GetAllBrands(BrandQueryModel queryModel);
    }
}