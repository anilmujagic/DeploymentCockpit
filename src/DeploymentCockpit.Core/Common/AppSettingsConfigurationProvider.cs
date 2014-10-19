using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using Insula.Common;

namespace DeploymentCockpit.Common
{
    public class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        public string GetString(string settingKey, string defaultValue = null)
        {
            var value = this.GetSetting(settingKey);
            if (!value.IsNullOrEmpty())
                return value;

            return defaultValue;
        }

        public bool? GetBoolean(string settingKey, bool? defaultValue = null)
        {
            var value = this.GetSetting(settingKey);
            bool boolValue;
            if (!value.IsNullOrEmpty())
                if (bool.TryParse(value, out boolValue))
                    return boolValue;

            return defaultValue;
        }

        public int? GetInteger(string settingKey, int? defaultValue = null)
        {
            var value = this.GetSetting(settingKey);
            int intValue;
            if (!value.IsNullOrEmpty())
                if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
                    return intValue;

            return defaultValue;
        }

        private string GetSetting(string settingKey)
        {
            try
            {
                return ConfigurationManager.AppSettings[settingKey];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetAllMessages());
            }

            return null;
        }
    }
}
