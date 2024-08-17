
namespace Domain.Constants
{
    public class PublicConstants
    {        
        public const string Admin = "admin";
        public const int PageNumber = 1;
        public const int PageSize = 10;
        public const int GuidMaxLength = 36; // حداکثر تعداد کاراکترهای فیلد های جی یو آی دی
        public const int DescriptionMaxLength = 255; // حداکثر تعداد کاراکترهای فیلد های توضیحات
        public const int ByteKilo = 1024;
        public const int ByteMega = 1024 * 1024;
        public const int FileMaxSize = ByteMega * 4; // 4M

        /// <summary>
        /// برای پیدا کردن نام مشخصات در کلاس های Validator
        /// </summary>
        public const string PropertyName = "{PropertyName}";

        public const string wwwrootFolder = "wwwroot";
        public const string UploadsFolder = "Uploads";
    }
}
