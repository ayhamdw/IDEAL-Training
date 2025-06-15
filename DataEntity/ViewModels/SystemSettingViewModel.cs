using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Models.ViewModels
{
    public class SystemSettingViewModel
    {
        public SystemSettingViewModel()
        {

        }
        public SystemSettingViewModel(SystemSetting setting)
        {
            Id = setting.Id;
            Name = setting.Name;
            Value = setting.Value;
            CreatedBy = setting.CreatedBy;
            CreatedOn = setting.CreatedOn;
            Status = setting.Status;

        }
        public int Id { get; set; }
        public int? TypeId { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? SuperAdminId { get; set; }
    }
}
