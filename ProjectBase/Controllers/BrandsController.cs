using DataEntity.Models;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Generic;
using ProjectBase.Services.IServices;
using System.Configuration;

namespace ProjectBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly IBrandService _brandService;

    public BrandsController(IUserProfileService userProfileService , IHttpContextAccessor httpContextAccessor , IBrandService brandService)
    {
        _userProfileService = userProfileService;
        _brandService = brandService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBrands([FromQuery] BrandQueryModel query)
    {
        var brands = await _brandService.GetAllBrands(query);
        if (brands.Count == 0)
        {
            return NotFound(ApiResponse<string>.FailedResponse("No brands found"));
        }
        return Ok(ApiResponse<object>.SuccessResponse(brands, "Brands retrieved successfully."));
    }
}