using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProjectBase.Services.Interfaces;
using ProjectBase.Core.Enums;
using ProjectBase.Generic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using Services.Helpers;
namespace ProjectBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPromotions([FromQuery] bool? isActive)
        {
            var promotions = await _promotionService.GetPromotionsAsync(isActive);
            return Ok(ApiResponse<object>.SuccessResponse(promotions));
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] PromotionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthenticationHelper.GetUserId(User);
            var createdBy = AuthenticationHelper.GetUserEmail(User);
            var promotionId = await _promotionService.CreatePromotionAsync(model, createdBy);

            return Ok(ApiResponse<object>.SuccessResponse(new
            {
                message = "Promotion created successfully.",
                promotionId
            }));
        

    }
}
}
