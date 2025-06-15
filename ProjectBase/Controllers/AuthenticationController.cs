using DataEntity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using ProjectBase.Core.Enums;
using ProjectBase.Generic;
using ProjectBase.Services.BackgroundServices;
using ProjectBase.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using DataEntity.ViewModels;
using ProjectBase.Services.Services;
using ProjectBase.Core;

namespace ProjectBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly ProjectBaseContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAwsServices _awsServices;
        private readonly IUserProfileService _userProfileService;
        private readonly IStringLocalizer<AuthenticationController> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly QueuedBackgroundService _backgroundService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetService _passwordResetService;



        public AuthenticationController(IConfiguration configuration,
            ProjectBaseContext ProjectBaseContext,
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAwsServices awsServices,
            IUserProfileService userProfileService, IStringLocalizer<AuthenticationController> localizer,
            IHttpContextAccessor httpContextAccessor, QueuedBackgroundService backgroundService, IPasswordResetService passwordResetService,
            IEmailService emailService)
        {
            _context = ProjectBaseContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _awsServices = awsServices;
            _userProfileService = userProfileService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _backgroundService = backgroundService;
            _configuration = configuration;
            _emailService = emailService;
            _passwordResetService = passwordResetService;

        }


        [HttpPost("login")]
        //[EnableRateLimiting("login")] // Enable rate limiting for this endpoint 5 attempts/15 mins      
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<object>.FailedResponse("Invalid Login Data", errors));
            }

            var identityUser = await _userManager.FindByEmailAsync(model.Email); //Sign in with email insted of username
            if (identityUser == null)
                return Unauthorized(ApiResponse<object>.FailedResponse("Invalid Email."));

            var passwordValid = await _userManager.CheckPasswordAsync(identityUser, model.Password);
            if (!passwordValid)
                return Unauthorized(ApiResponse<object>.FailedResponse("Invalid password."));

            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (userProfile == null)
                return Unauthorized(ApiResponse<object>.FailedResponse("Invalid Username or Password"));

            var (token, expiry) = GenerateJwtToken(userProfile.Id, identityUser.Email);

            var expiresInSeconds = (int)(expiry - DateTime.UtcNow).TotalSeconds;

            var responseData = new
            {
                Token = token,
                expiresIn = expiresInSeconds,
                User = new
                {
                    userProfile.Id,
                    userProfile.Email,

                    userProfile.FirstName,
                    userProfile.LastName,
                }
            };

            return Ok(ApiResponse<object>.SuccessResponse(responseData, "Login successful"));
        }


        private (string Token, DateTime Expiry) GenerateJwtToken(int userId, string? email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };
            var expiry = DateTime.UtcNow.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: expiry
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return (tokenHandler.WriteToken(token), expiry);
        }


        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await _signInManager.SignOutAsync();
        //        return Ok(new ApiResponse<string>("Logged out successfully."));
        //    }
        //    return Unauthorized(new ApiResponse<string>("User is not logged in."));
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailedResponse("Invalid Registration"));
            var userExistsResponse = await CheckIfUserExists(model.Email);
            if (userExistsResponse != null)
                return userExistsResponse;

            var userEmailExistResponse = await CheckIfEmailExists(model);
            if (userEmailExistResponse != null) return userEmailExistResponse;

            // var profileImagePath = await UploadProfileImage(model.Photo);
            const string profileImagePath = Constants.Urls.profileImagePath;
            var registrationResult = await CreateUserAndProfile(model, profileImagePath);

            if (!registrationResult.IsSuccess)
                return BadRequest(ApiResponse<string>.FailedResponse(registrationResult.Message));


            return Ok(ApiResponse<dynamic>.SuccessResponse(new { registrationResult.data },
                "Registration successful"));
        }

        #region Register

        private async Task<(bool IsSuccess, string Message, UserProfile data)> CreateUserAndProfile(
            RegisterViewModel model, string? profileImagePath)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                // Create Identity User
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = false
                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (!identityResult.Succeeded)
                {
                    var errorMessages = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                    return (false, $"Failed to create user: {errorMessages}", null);
                }

                // Create UserProfile
                var userProfile = new UserProfile
                {
                    Username = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.UtcNow,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication",
                    new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
                const string subject = "Verify Your Email";
                var body = EmailContent.GenerateEmailBody(confirmationLink);
                _backgroundService.QueueWorkItemAsync(async token =>
                {
                    try
                    {
                        await _emailService.SendEmailAsync(model.Email, subject, body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Background email sending failed: {ex.Message}");
                    }
                });
                await _context.UserProfiles.AddAsync(userProfile);
                await _context.SaveChangesAsync();
                // await _emailService.SendEmailAsync(model.Email, subject, body);
                transactionScope.Complete();

                return (true, "User registered successfully, please check your Email", userProfile);
            }
            catch (Exception ex)
            {
                return (false, $"Transaction failed: {ex.Message}. Inner: {ex.InnerException?.Message}", null);
            }
        }

        private async Task<string?> UploadProfileImage(IFormFile photo)
        {
            if (photo == null)
                return null;

            return await _awsServices.UploadToS3("", photo);
        }

        private async Task<IActionResult?> CheckIfUserExists(string username)
        {
            var usernameExists = await _userProfileService.UsernameExistsAsync(username);
            return usernameExists
                ? Conflict(ApiResponse<string>.FailedResponse($"User UserName: '{username}' already exists."))
                : null;
        }

        private async Task<IActionResult?> CheckIfEmailExists(RegisterViewModel model)
        {
            var emailExists = await _userProfileService.EmailExistsAsync(model.Email);
            return emailExists
                ? Conflict(ApiResponse<string>.FailedResponse($"Email: '{model.Email}' already exists."))
                : null;
        }


        #endregion

        [HttpGet("verify")]
        public async Task<IActionResult> ConfirmEmail(string? userId, string? token)
        {
            if (userId == null || token == null)
            {
                return BadRequest(ApiResponse<object>.FailedResponse("Invalid Registration"));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(ApiResponse<object>.FailedResponse("User Not Found"));
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Redirect(Constants.Urls.LoginPage);
            }

            return BadRequest(ApiResponse<object>.FailedResponse("Confirm Email Failed"));

        }
        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestViewModel model)
        {
            await _passwordResetService.RequestPasswordResetAsync(model.Email);
            return Ok(ApiResponse<object>.SuccessResponse("If the email exists, a reset token has been sent."));

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var success = await _passwordResetService.ResetPassword(
                model.Email,
                model.Token,
                model.NewPassword
            );

            return success
             ? Ok(ApiResponse<object>.SuccessResponse("Password reset successfully."))

                : BadRequest("Invalid token or user.");
        }
    }
}