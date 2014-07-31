using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;

namespace DeploymentCockpit.Common
{
    public static class DomainContext
    {
        private static IConfigurationProvider _configurationProvider;
        public static IConfigurationProvider ConfigurationProvider
        {
            get
            {
                if (_configurationProvider == null)
                    throw new Exception("ConfigurationProvider is not available.");

                return _configurationProvider;
            }
            set
            {
                _configurationProvider = value;
            }
        }

        public static string ServerKey
        {
            get { return ConfigurationProvider.GetString("ServerKey"); }
        }

        #region Server-only Settings

        public static string PasswordEncryptionKey
        {
            get { return ConfigurationProvider.GetString("PasswordEncryptionKey"); }
        }
        public static string PasswordEncryptionSalt
        {
            get { return ConfigurationProvider.GetString("PasswordEncryptionSalt"); }
        }

        #endregion

        #region Target-only Settings

        public static string TargetKey
        {
            get { return ConfigurationProvider.GetString("TargetKey"); }
        }
        public static string IPAddress
        {
            get { return ConfigurationProvider.GetString("IPAddress"); }
        }
        public static int PortNumber
        {
            get { return ConfigurationProvider.GetInteger("PortNumber", 29180).Value; }
        }

        #endregion

        public static string DateTimeFormatString
        {
            get { return ConfigurationProvider.GetString("DateTimeFormatString", "yyyy-MM-dd HH:mm:ss"); }
        }
        public static string VariablePlaceholderPrefix
        {
            get { return ConfigurationProvider.GetString("VariablePlaceholderPrefix", "__"); }
        }
        public static string VariablePlaceholderSuffix
        {
            get { return ConfigurationProvider.GetString("VariablePlaceholderSuffix", "__"); }
        }
    }
}
