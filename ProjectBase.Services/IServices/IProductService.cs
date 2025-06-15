using DataEntity.Models;
using DataEntity.ViewModels;

namespace ProjectBase.Services.IServices;

public interface IProductService
{
    CategoryProductsViewModel GetProducts(ProductQueryModel productQueryModel);
    Task<ProductViewModel> CreateProduct (CreateProductViewModel createProductViewModel);
}