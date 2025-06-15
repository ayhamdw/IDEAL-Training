using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectBase.Generic
{
    public static class ApiResponseHelper
    {
        public static IActionResult CreateResponse<T>(this ControllerBase controller, ApiResponse<T> response)
        {
            if (!response.Success)
            {
                return controller.BadRequest(response);
            }
            return controller.Ok(response);
        }
    }

}
