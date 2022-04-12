using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarCaseTest.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
        {
            var messageAttr = GetAttributeOfType<DisplayAttribute>(val);

            return messageAttr?.Name ?? val.ToString();
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Length <= 0) return null;
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes.FirstOrDefault();
        }
    }
}
