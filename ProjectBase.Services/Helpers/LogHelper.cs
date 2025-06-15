using DataEntity.Models;
using ProjectBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class LogHelper
    {
        public static void AddSystemLog(SystemLog log)
        {
            using (var db = new ProjectBaseContext())
            {
                try
                {
                    db.SystemLogs.Add(log);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //TODO: Send Email incase of the failure of adding appliaction log
                }
            }
        }


        public static void LogException(string username, Exception ex, string component)
        {
            try
            {
                SystemLog log = new SystemLog
                {
                    Name = ex.Message,
                    CreatedOn = DateTime.Now,
                    CreatedBy = username,
                    Component = component,
                    StackTrace = $"InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}",
                    Status = (int)GeneralEnums.StatusEnum.Active
                };
                AddSystemLog(log);
            }
            catch (Exception exx)
            {

            }
        }
    }
}
