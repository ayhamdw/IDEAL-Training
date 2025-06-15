using Microsoft.AspNetCore.Identity;
using ProjectBase.Services.IServices;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using ProjectBase.Core;

namespace ProjectBase.Services.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public PasswordResetService(UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return; 

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetUrl = $"{Constants.Urls.ResetPasswordBaseUrl}?token={token}";
            var emailBody = $"Click to reset your password: <a href='{resetUrl}'>{resetUrl}</a>";

            await _emailService.SendEmailAsync(email,"Reset your password", emailBody);
        }

        public async Task<bool> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;


            foreach (var validator in _userManager.PasswordValidators)
            {
                var validation = await validator.ValidateAsync(_userManager, user, newPassword);
                if (!validation.Succeeded) return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}