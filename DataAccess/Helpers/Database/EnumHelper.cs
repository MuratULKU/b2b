using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Database
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            DisplayAttribute attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();

            if (attribute == null)
            {
                return value.ToString();
            }

            string propValue = attribute.Name;
            return propValue.ToString();
        }
    }
}
