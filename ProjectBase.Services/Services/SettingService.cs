using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using ProjectBase.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBase.Core;
using ProjectBase.Core.Enums;
using DataEntity.Models;
using ProjectBase.Services.IServices;

namespace ProjectBase.Services.Services
{
    public class SettingService : ISettingService
    {
        private readonly ProjectBaseContext _context;

        public SettingService(ProjectBaseContext context)
        {
            _context = context;
        }

       

        public SystemSettingViewModel GetOrCreate(string name, string defaultValue, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            var setting = _context.SystemSettings.FirstOrDefault(r =>
                r.Name == name && r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    Value = defaultValue,
                    Name = name
                };
                _context.SystemSettings.Add(setting);
                _context.SaveChanges();

                if (languageId != (int)GeneralEnums.LanguageEnum.English)
                {
                    //var systemTran = new SystemSettingTranslation()
                    //{   
                    //    Name = name,
                    //    Value = defaultValue,
                    //    LanguageId = languageId,
                    //    SettingId = setting.Id
                    //};
                    //_context.SystemSettingTranslations.Add(systemTran);
                    _context.SaveChanges();
                }

                var sysSetting = new SystemSettingViewModel()
                {
                    Id = setting.Id,
                    Name = name,
                    Value = defaultValue
                };

                return sysSetting;
            }
            else
            {
                return new SystemSettingViewModel()
                {
                    Id = setting.Id,
                    Name = setting.Name,
                    Value = setting.Value
                };
            }
        }
        public SystemSettingViewModel GetOrCreate(string name, string defaultValue,ProjectBaseContext ProjectBaseContext, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            var setting = ProjectBaseContext.SystemSettings.FirstOrDefault(r =>
                r.Name == name && r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    Value = defaultValue,
                    Name = name
                };
                ProjectBaseContext.SystemSettings.Add(setting);
                ProjectBaseContext.SaveChanges();

                if (languageId != (int)GeneralEnums.LanguageEnum.English)
                {
                    //var systemTran = new SystemSettingTranslation()
                    //{
                    //    Name = name,
                    //    Value = defaultValue,
                    //    LanguageId = languageId,
                    //    SettingId = setting.Id
                    //};
                    //ProjectBaseContext.SystemSettingTranslations.Add(systemTran);
                    ProjectBaseContext.SaveChanges();
                }

                var sysSetting = new SystemSettingViewModel()
                {
                    Id = setting.Id,
                    Name = name,
                    Value = defaultValue
                };

                return sysSetting;
            }
            else
            {
                return new SystemSettingViewModel()
                {
                    Id = setting.Id,
                    Name = setting.Name,
                    Value = setting.Value
                };
            }
        }

        public async Task<List<SystemSettingViewModel>> GetMultipleSystemSettings(string[] name, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
                var allSetting = _context.SystemSettings.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Select(r => r.Name);
                var settingNoAdded = name.Where(m => !allSetting.Contains(m));

                foreach (var item in settingNoAdded)
                {
                    GetOrCreate(item, "", languageId);
                }

            //var setting = _context.SystemSettings.Include(r => r.SystemSettingTranslations).Where(r =>
            //      name.Contains(r.Name) && r.Status == (int)GeneralEnums.StatusEnum.Active);

            //if (languageId != CultureHelper.GetDefaultLanguageId())
            //{
            //    foreach (var item in setting)
            //    {
            //        var trans = item.SystemSettingTranslations.FirstOrDefault(r => r.LanguageId == languageId);
            //        if (trans != null)
            //        {
            //            item.Name = trans.Name;
            //            item.Value = trans.Value;
            //        }
            //    }
            //}
            //return await setting.Select(r => new SystemSettingViewModel(r)).ToListAsync();
            return null;
        }

        public bool SetSettingValue(string name, string value)
        {
            var setting = _context.SystemSettings.FirstOrDefault(s =>
                s.Name == name && s.Status == (int)GeneralEnums.StatusEnum.Active);
            if (setting == null)
            {
                return false;
            }
            setting.Value = value;
            _context.SaveChanges();
            return true;
        }
    }
}
