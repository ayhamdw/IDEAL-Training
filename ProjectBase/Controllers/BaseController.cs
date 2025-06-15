using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectBase.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProjectBase.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected int? UserId { get; private set; }
        protected string? UserName { get; private set; }
        protected string? Language { get; private set; }

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            if (!ValidateAndExtractToken())
            {
                // throw new UnauthorizedAccessException("Invalid or missing token.");
            }
        }

        private bool ValidateAndExtractToken()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null) return false;
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var Language = httpContext.Request.Headers["Accept-Language"].FirstOrDefault()?? Constants.Languages.English;

                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                
                UserId = int.TryParse(principal.FindFirst("userId")?.Value, out int id) ? id : null;
                UserName = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                return UserId != null && !string.IsNullOrEmpty(UserName);
            }
            catch
            {
                return false;
            }
        }
    }
}
