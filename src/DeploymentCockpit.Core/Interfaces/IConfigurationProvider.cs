using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetString(string settingKey, string defaultValue = null);
        bool? GetBoolean(string settingKey, bool? defaultValue = null);
        int? GetInteger(string settingKey, int? defaultValue = null);
    }
}
