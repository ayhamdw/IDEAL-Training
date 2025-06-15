using Microsoft.AspNetCore.Mvc;
using ProjectBase.Services.IServices;
using ProjectBase.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.Helpers;
using DataEntity.Models;
using DataEntity.ViewModels;


namespace AdvertisementFeature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService _service;

        public AdvertisementsController(IAdvertisementService service)
        {
            _service = service;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAdvertisements()
        {
            var result =await _service.GetActiveAdvertisements();
            return Ok(ApiResponse<object>.SuccessResponse(result));
        }

        [HttpPost]
        //[Authorize]

        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementViewModel model)
        {
            var createdBy = "test";
            //var createdBy = AuthenticationHelper.GetUserEmail(User);
            var id = await _service.CreateAdvertisement(model, createdBy);
            return Ok(ApiResponse<object>.SuccessResponse(id, "Advertisement created"));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAdvertisement([FromBody] AdvertisementUpdateViewModel model)
        {
            var success = await _service.UpdateAdvertisement(model);
            if (!success)
                return NotFound(ApiResponse<object>.FailedResponse("Advertisement not found"));

            return Ok(ApiResponse<object>.SuccessResponse("Advertisement updated"));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAdvertisement([FromBody] AdvertisementDeleteViewModel model)
        {
            var result = await _service.DeleteAdvertisement(model.Id);
            if (!result)
                return NotFound(ApiResponse<object>.FailedResponse("Advertisement not found"));

            return Ok(ApiResponse<object>.SuccessResponse("Advertisement deactivated"));
        }

    }
}
