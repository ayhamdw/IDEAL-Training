

using DataEntity.Models;
using DataEntity.Models.ViewModels;
using ProjectBase.Core.Enums;

namespace ProjectBase.Services.IServices
{
    public interface ISettingService
    {
        SystemSettingViewModel GetOrCreate(string name, string defaultValue, int languageId = (int)GeneralEnums.LanguageEnum.English);
        SystemSettingViewModel GetOrCreate(string name, string defaultValue, ProjectBaseContext coffeeShopContext, int languageId = (int)GeneralEnums.LanguageEnum.English);
        Task<List<SystemSettingViewModel>> GetMultipleSystemSettings(string[] name, int languageId = (int)GeneralEnums.LanguageEnum.English);
        bool SetSettingValue(string name, string value);
    }
}
