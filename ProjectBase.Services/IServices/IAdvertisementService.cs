using DataEntity.Models;
using DataEntity.ViewModels;
using System.Collections.Generic;

namespace ProjectBase.Services.IServices
{
    public interface IAdvertisementService
    {
        Task<List<AdvertisementViewModel>> GetActiveAdvertisements();
        Task<int> CreateAdvertisement(AdvertisementViewModel model, string createdBy);
        Task<bool> UpdateAdvertisement(AdvertisementUpdateViewModel model); 
        Task<bool> DeleteAdvertisement(int id);
    }
}
