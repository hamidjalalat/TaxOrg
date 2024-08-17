using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Shared;

namespace ViewModels.Users
{
    public class UserManageStatisticReportInputParamsViewModel : PublicViewModel
    {
        public string UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int TotalCount { get; set; }
    }
}
