using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nazm.Extensions
{

    public static class ExtensionMethod
    {

        public static string ToPersianDate(this System.DateTime dt)
        {
            try
            {
                System.Globalization.PersianCalendar persianDate = new();
                int year = persianDate.GetYear(dt);
                int month = persianDate.GetMonth(dt);
                int day = persianDate.GetDayOfMonth(dt);

                return string.Format("{0}/{1}/{2}", year, month.ToString("00"), day.ToString("00"));//.ToPersianNumber();
            }
            catch (Exception)
            {
                return "";
            }

        }

        public static string ToPersianDateTime(this System.DateTime? dt)
        {
            try
            {
                if (!dt.HasValue)
                {
                    return "";
                }
                System.Globalization.PersianCalendar persianDate = new();

                int year = persianDate.GetYear(dt.Value);
                int month = persianDate.GetMonth(dt.Value);
                int day = persianDate.GetDayOfMonth(dt.Value);
                return string.Format("{0}/{1}/{2} {3}:{4}", year, month.ToString("00"),
                    day.ToString("00"), dt.Value.Hour.ToString("00"), dt.Value.Minute.ToString("00"));
            }
            catch (Exception)
            {
                return "";
            }

        }

        public static string ToPersianDateTime(this System.DateTime dt)
        {
            try
            {
                System.Globalization.PersianCalendar persianDate = new();

                int year = persianDate.GetYear(dt);
                int month = persianDate.GetMonth(dt);
                int day = persianDate.GetDayOfMonth(dt);
                return string.Format("{0}/{1}/{2} {3}:{4}", year, month.ToString("00"),
                    day.ToString("00"), dt.Hour.ToString("00"), dt.Minute.ToString("00"));
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetDisplayNameAttribute<T>(this T className, string propertyName) where T : class
        {
            if (className == null)
                return propertyName;

            var property = className.GetType().GetProperty(propertyName);

            if (property == null)
                return propertyName;

            var attributes = property.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0
              ? ((DisplayAttribute)attributes[0]).Name ?? propertyName
              : propertyName;
        }

        public static ValidationResult GetValidationResult<T>(this T className, string errorMessage, string properyName) where T : class
        {
            return new ValidationResult(string.Format(errorMessage, className.GetDisplayNameAttribute(properyName)), new[] { properyName });
        }

        public static ValidationResult GetValidationResult<T>(this T className, string errorMessage, string properyName, string properyFullName) where T : class
        {
            return new ValidationResult(string.Format(errorMessage, className.GetDisplayNameAttribute(properyName)), new[] { properyFullName ?? properyName });
        }


    }

    public static class EnumExtensions
    {
        public static string GetDescriptionAttribute(this Enum value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
              ? ((DescriptionAttribute)attributes[0]).Description
              : value.ToString();
        }
    }


}