﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nazm.Extensions
{
    public static class Extension
    {
        public static string ToPersianST(this string value)
        {
            return value.Replace("آ", "ا").Replace("ئ", "ي").Replace("ء", "");
        }
        public static string GetIntOnly(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            string pattern = "[ئ]|[ء]|[ي]|[ی]|[ه]|[و]|[ن]|[م]|[ل]|[گ]|[ک]|[ق]|[ف]|[غ]|[ع]|[ظ]|[ط]|[ض]|[ص]|[ش]|[س]|[ژ]|[ز]|[ر]|[ذ]|[د]|[خ]|[ح]|[چ]|[ج]|[ث]|[ت]|[پ]|[ب]|[آ]|[ا]|[؟]|[ك]|[ڪ]";
            var rgx = new Regex(pattern);
            return rgx.Replace(value, "");
        }

        public static string GetCharOnly(this string value)
        {
            //if (string.IsNullOrEmpty(value)) return string.Empty;

            //for (var i = 0; i < 10; i++)
            //    value = value.Replace(char.ConvertFromUtf32(0x30 + i), ""); // ۰

            //return value;
            var rgx = new Regex(@"\d");

            return rgx.Replace(value, "");

        }

        public static byte ToByte(this string value)
        {
            byte result = 0;
            byte.TryParse(value, out result);
            return result;
        }

        public static short ToInt16(this string value)
        {
            short result = 0;
            short.TryParse(value, out result);
            return result;
        }

        public static int ToInt32(this string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }

        public static long ToInt64(this string value)
        {
            long result = 0;
            long.TryParse(value, out result);
            return result;
        }

        public static bool IsValidNationalCode(this string nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (string.IsNullOrEmpty(nationalCode))
                throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = num0 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;
            var c = b % 11;

            return c < 2 && a == c || c >= 2 && 11 - c == a;
        }
    }


}
