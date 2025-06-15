using DataEntity.Models;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Services.IServices;
using DataEntity.ViewModels;

namespace ProjectBase.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ProjectBaseContext _context;
        public UserProfileService(ProjectBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.UserProfiles
                .AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> EmailExistsAsync(string modelEmail)
        {
            var check = await _context.UserProfiles.AnyAsync(u => u.Email != null && u.Email.ToLower() == modelEmail.ToLower());
            return check;
        }

        public async Task<UserProfileViewModel> GetUserByUserName(string username)
        {
            var userProfile = await _context.UserProfiles
                                 .FirstOrDefaultAsync(u => u.Username == username);
            return userProfile != null ? new UserProfileViewModel(userProfile) : new UserProfileViewModel();
        }
    }
}
