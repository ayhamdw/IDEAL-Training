using DataEntity.Models;
using ProjectBase.Core.Enums;
using X.PagedList;

namespace ProjectBase.Services.IServices
{
    public interface IUserProfileService
    {
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string modelEmail);
    }
}
