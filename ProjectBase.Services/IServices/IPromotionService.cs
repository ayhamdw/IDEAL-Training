// ProjectBase/Services/Interfaces/IPromotionService.cs
using DataEntity.Models;
using DataEntity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBase.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetPromotionsAsync(bool? isActive);
        Task<int> CreatePromotionAsync(PromotionCreateViewModel model, string createdBy);
    }
}