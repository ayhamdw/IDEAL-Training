using System.Threading.Tasks;

namespace ProjectBase.Services.IServices
{
    public interface IPasswordResetService
    {
        Task RequestPasswordResetAsync(string email);
        Task<bool> ResetPassword(string email, string token, string newPassword);
    }
}