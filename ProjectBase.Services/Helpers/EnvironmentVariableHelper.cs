using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Services.Helpers
{
    public static class EnvironmentVariableHelper
    {
        public static string GetEnvironmentVariableOrDefault(string variableName, string defaultValue)
        {
            // Retrieve the environment variable value
            var value = Environment.GetEnvironmentVariable(variableName);

            // Return the value if it's not null or empty; otherwise, return the default value
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }

}
