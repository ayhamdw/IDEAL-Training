using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Generic;
using ProjectBase.Services.IServices;

namespace ProjectBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    
    private readonly ICategoriesService _categoriesService;
    private readonly IUserProfileService _userProfileService;
    
    public CategoriesController(IConfiguration configuration,  ICategoriesService categoriesService, IUserProfileService userProfileService) 
    {
        _categoriesService = categoriesService;
        _userProfileService = userProfileService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0 || categories == null) return NotFound(ApiResponse<object>.FailedResponse("No categories found"));
        return Ok(ApiResponse<object>.SuccessResponse(categories , "Successfully Categories retrieved"));
    }

    [HttpGet("{id:int}/products")]
    public async Task<IActionResult> GetCategoryById(int id , [FromQuery] CategoryQueryModel query)
    {
        var categories = await _categoriesService.GetCategoryProductsById(id ,query);
        if (categories == null) return NotFound(ApiResponse<object>.FailedResponse("No category found"));
        return Ok(ApiResponse<object>.SuccessResponse(categories , "Successfully retrieved"));
    }

    [HttpPost]

    public async Task<IActionResult> CreateCategoryAsync([FromBody]CreateCategoryViewModel category)
    {
        
            var newCategory = await _categoriesService.AddNewCategory(category);
            if (newCategory == null) return NotFound(ApiResponse<object>.FailedResponse("No category found"));
            return Created("", ApiResponse<object>.SuccessResponse(newCategory, "Successfully created new category"));
        
    }
}