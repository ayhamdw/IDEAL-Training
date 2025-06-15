using DataEntity.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using ProjectBase.Core.Enums;
using ProjectBase.Generic;
using ProjectBase.Services.Helpers;
using ProjectBase.Services.IServices;
using System.Diagnostics;
using System.Globalization;

namespace ProjectBase.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ProjectBaseContext _context;

        public HomeController(IConfiguration configuration,ProjectBaseContext context, IUserProfileService userProfileService,
            IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor) 
        {
            _userProfileService = userProfileService;
            _webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _context = context;
        }

       
    }
}

