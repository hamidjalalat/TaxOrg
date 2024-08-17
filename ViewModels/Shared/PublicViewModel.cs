using Domain.Anemic.Common;
using Domain.Constants;
using System.Collections.Generic;

namespace ViewModels.Shared
{
    public class PublicViewModel
    {
        public FilterParams? FilterParams { get; set; } = null;
        public int PageNumber { get; set; } = PublicConstants.PageNumber;
        public int PageSize { get; set; } = PublicConstants.PageSize;
        public int? MenuId { get; set; }
        /// <summary>
        /// فقط برای تبدیل داده ها به فرمت مورد نظر فایل (مثل خروجی به اکسل) کاربرد دارد
        /// </summary>
        public Domain.Enums.FileExportTypeEnum? FileExportType { get; set; } = null;

        public IList<ExcelExportColumns>? FileExportColumns { get; set; } = null;
    }
}
