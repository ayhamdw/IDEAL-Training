using Microsoft.AspNetCore.Http;


namespace ProjectBase.Services.IServices
{
    public interface IAwsServices
    {
        Task <string> UploadToS3(string key, IFormFile image);
    }
}
