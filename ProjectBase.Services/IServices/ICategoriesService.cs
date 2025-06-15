using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProjectBase.Services.IServices;

public interface ICategoriesService
{
    Task<List<CategoryViewModel>> GetAllCategories();

    Task<CategoryProductsViewModel> GetCategoryProductsById(int id,CategoryQueryModel queryModel);
    Task<CategoryViewModel> AddNewCategory(CreateCategoryViewModel createCategoryViewModel);
}