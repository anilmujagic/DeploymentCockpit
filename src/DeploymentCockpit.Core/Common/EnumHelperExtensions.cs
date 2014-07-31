using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Common
{
    public static class EnumHelperExtensions
    {
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToEnum<T>(this string value, T defaultValue)
            where T : struct
        {
            T enumValue;

            if (Enum.TryParse<T>(value, out enumValue))
                return enumValue;

            return defaultValue;
        }
    }
}
