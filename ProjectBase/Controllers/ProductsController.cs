using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Generic;
using ProjectBase.Services.IServices;

namespace ProjectBase.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly IProductService _productService;
    public ProductsController(IConfiguration configuration, IHttpContextAccessor  httpContextAccessor, IProductService productService) : base(configuration, httpContextAccessor)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery]ProductQueryModel query)
    {
        var result = _productService.GetProducts(query);
        return Ok(ApiResponse<object>.SuccessResponse(result , "Product Fetched Successfully"));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductViewModel createProduct)
    {
        var result = await _productService.CreateProduct(createProduct);
        return Ok(ApiResponse<object>.SuccessResponse(result , "Product Created Successfully"));
    }
}