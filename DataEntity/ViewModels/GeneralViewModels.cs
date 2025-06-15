using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.ViewModels
{
    public class GeneralViewModels
    {
        public class CheckResult
        {
            public bool Exists { get; set; }
            public string Message { get; set; } = string.Empty;
        }
    }
}
