using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nazm
{
    public class DateConversionClass
    {
        #region Static Methods

        private static DateConversionClass _instance;
        public static DateConversionClass GetInstance()
        {
            if (_instance == null)
                _instance = new DateConversionClass();
            return _instance;
        }
		#endregion

		public DateConversionClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static System.DateTime DateTimeNow
        {
            get
            {
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

                var currentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                var englishCulture = new System.Globalization.CultureInfo(name: "en-US");

                System.Threading.Thread.CurrentThread.CurrentCulture = englishCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = englishCulture;

                var result = System.DateTime.Now;
                //System.DateTime result = Convert.ToDateTime(System.DateTime.Now, englishCulture);

                System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = currentUICulture;

                return result;
            }
        }

        string[] GregorianDayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        string[] GregorianDayMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        string[] PersianDayNames = { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
        string[] PersianDayMonths = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        string[] QamariDayNames = { "الأحد‬", "الاثنین‬", "الثلاثاء", "الأربعاء", "الخمیس‬", "الجمعة", "السبت‬" };
        string[] QamariDayMonths = { "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاول", "جمادی الثانی", "رجب", "شعبان", "رمضان", "شوال", "ذیقعده", "ذیحجه" };

        /// <summary>
        /// روز جاری هفته
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <param name="culture">en , fa, ar</param>
        /// <returns></returns>
        public string GetDayOfWeek(System.DateTime date, string culture)
        {
            Dictionary<string, string[]> DayOfWeeks = new Dictionary<string, string[]>();
            DayOfWeeks.Add("en", GregorianDayNames);
            DayOfWeeks.Add("fa", PersianDayNames);
            DayOfWeeks.Add("ar", QamariDayNames);

            DayOfWeek dayOfWeek = date.DayOfWeek;

            string strReturn = DayOfWeeks[culture][(int)dayOfWeek];
            return strReturn;
        }

        /// <summary>
        /// کپی رایت براساس سال
        /// </summary>
        /// <param name="startYear">سال شروع</param>
        /// <returns></returns>
        public string GetCopyRightFormatYear(int startYear)
        {
            System.DateTime dt = GetGregorianDateToday();
            PersianCalendar persianCalendar = new PersianCalendar();

            int endYear = persianCalendar.GetYear(dt);
            string strReturn = string.Empty;

            if (startYear == endYear)
            {
                strReturn = startYear.ToString();
            }
            else
            {
                strReturn = string.Format("{0} - {1}", startYear, endYear);
            }

            return strReturn;
        }


        #region Gregorian Date

        /// <summary>
        /// تاریخ میلادی برای روز جاری
        /// </summary>
        /// <returns></returns>
        public System.DateTime GetGregorianDateToday()
        {
            return System.DateTime.Now;
        }

        /// <summary>
        /// تاریخ میلادی برای روز جاری
        /// </summary>
        /// <returns></returns>
        public string GetGregorianDateTodayFormatString()
        {
            System.DateTime dt = GetGregorianDateToday();

            string strReturn = string.Format("{0}/{1}/{2}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
            return strReturn;
        }

        /// <summary>
        /// تاریخ میلادی برای روز جاری با نمایش روز
        /// </summary>
        /// <returns></returns>
        public string GetGregorianDateTodayFormatDay()
        {
            System.DateTime dt = GetGregorianDateToday();

            string strReturn = string.Format("{0}، {1} {2}, {3}",
                GetDayOfWeek(dt, "en"),
                GregorianDayMonths[dt.Month - 1],
                dt.Day,
                dt.Year);

            return strReturn;
        }

        // <summary>
        /// تاریخ شمسی به میلادی 
        /// </summary>
        /// <returns></returns>
        public System.DateTime GetGregorianDate(int year, int month, int day)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            System.DateTime dtReturn = new System.DateTime(year, month, day, persianCalendar);

            return dtReturn;
        }

        #endregion 


        #region Persian Date

        /// <summary>
        /// تاریخ شمسی برای روز جاری
        /// </summary>
        /// <returns></returns>
        public string GetPersianDateToday()
        {
            System.DateTime dt = GetGregorianDateToday();
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dt),
                persianCalendar.GetMonth(dt).ToString().PadLeft(2, '0'),
                persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

            return strReturn;
        }

        /// <summary>
        /// تاریخ شمسی برای روز جاری با نمایش روز
        /// </summary>
        /// <returns></returns>
        public string GetPersianDateTodayFormatDay()
        {
            System.DateTime dt = GetGregorianDateToday();
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}، {1} {2} {3}",
                GetDayOfWeek(dt, "fa"),
                persianCalendar.GetDayOfMonth(dt),
                PersianDayMonths[persianCalendar.GetMonth(dt) - 1],
                persianCalendar.GetYear(dt));

            return strReturn;
        }

        /// <summary>
        /// تاریخ شمسی برای سال جاری
        /// </summary>
        /// <returns></returns>
        public string GetPersianDateCurrentYear()
        {
            System.DateTime dt = GetGregorianDateToday();
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}", persianCalendar.GetYear(dt));
            return strReturn;
        }

        // <summary>
        /// تاریخ میلادی به شمسی 
        /// </summary>
        /// <returns></returns>
        public string GetPersianDate(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dt),
                persianCalendar.GetMonth(dt).ToString().PadLeft(2, '0'),
                persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

            return strReturn;
        }

        // <summary>
        /// تاریخ شمسی با نمایش حروفی ماه و روز
        /// </summary>
        /// <returns></returns>
        public string GetPersianDateFormatMonthDay(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}، {1} {2} {3}",
                GetDayOfWeek(dt, "fa"),
                persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'),
                PersianDayMonths[persianCalendar.GetMonth(dt) - 1],
                persianCalendar.GetYear(dt));

            return strReturn;
        }

        // <summary>
        /// تاریخ شمسی با نمایش حروفی روز
        /// </summary>
        /// <returns></returns>
        public string GetPersianDateFormatDay(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}، {1}/{2}/{3}",
                GetDayOfWeek(dt, "fa"),
                persianCalendar.GetYear(dt),
                persianCalendar.GetMonth(dt).ToString().PadLeft(2, '0'),
                persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

            return strReturn;
        }

        // <summary>
        /// تاریخ شمسی بصورت عددی بدون کاراکتر
        /// </summary>
        /// <returns></returns>
        public int GetPersianDateFormatNumberOnly(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string strReturn = string.Format("{0}{1}{2}",
                persianCalendar.GetYear(dt),
                persianCalendar.GetMonth(dt).ToString().PadLeft(2, '0'),
                persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

            int intReturn = Convert.ToInt32(strReturn);

            return intReturn;
        }

        // <summary>
        /// سال تاریخ شمسی بصورت عددی
        /// </summary>
        /// <returns></returns>
        public int GetPersianDateYearNumber(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int intReturn = persianCalendar.GetYear(dt);

            return intReturn;
        }

        // <summary>
        /// ماه تاریخ شمسی بصورت عددی
        /// </summary>
        /// <returns></returns>
        public int GetPersianDateMonthNumber(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int intReturn = persianCalendar.GetMonth(dt);

            return intReturn;
        }

        // <summary>
        /// روز تاریخ شمسی بصورت عددی
        /// </summary>
        /// <returns></returns>
        public int GetPersianDateDayOfMonthNumber(System.DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int intReturn = persianCalendar.GetDayOfMonth(dt);

            return intReturn;
        }

        #endregion


        #region Qamari Date

        /// <summary>
        /// تاریخ قمری برای روز جاری
        /// </summary>
        /// <returns></returns>
        public string GetQamariDateToday()
        {
            System.DateTime dt = GetGregorianDateToday();
            HijriCalendar hijriCalendar = new HijriCalendar();
            string strReturn = string.Format("{0}/{1}/{2}",
                hijriCalendar.GetYear(dt),
                hijriCalendar.GetMonth(dt).ToString().PadLeft(2, '0'),
                hijriCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

            return strReturn;
        }

        /// <summary>
        /// تاریخ قمری برای روز جاری با نمایش روز
        /// </summary>
        /// <returns></returns>
        public string GetQamariDateTodayFormatDay()
        {
            System.DateTime dt = GetGregorianDateToday();
            HijriCalendar hijriCalendar = new HijriCalendar();

            string strReturn = string.Format("{0}، {1} {2} {3}",
                GetDayOfWeek(dt, "ar"),
                hijriCalendar.GetDayOfMonth(dt),
                QamariDayMonths[hijriCalendar.GetMonth(dt) - 1],
                hijriCalendar.GetYear(dt));

            return strReturn;
        }

        #endregion 
        
    }
}
