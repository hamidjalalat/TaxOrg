using Domain.Anemic.Entities;
using Domain.Enums;
using Nazm.Extensions;
using NazmMapping.Mappings;
using Resources;
using System;

namespace ViewModels.LoginHistories
{
    public class LoginHistoryViewModel : IMapFrom<LoginHistory>
    {
        public long Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public DateTime LogDate { get; set; }
        public string LogDateSH
        {
            get
            {
                return LogDate.ToPersianDateTime();
            }
        }
        public HistoryTypeEnum HistoryType { get; set; }
        public string HistoryTypeTitle
        {
            get
            {
                string title = string.Empty;
                switch (HistoryType)
                {
                    case Domain.Enums.HistoryTypeEnum.Login:
                        title = Buttons.Login;
                        break;
                    case Domain.Enums.HistoryTypeEnum.Logout:
                        title = Buttons.Logout;
                        break;
                    default:
                        title = "---";
                        break;
                }
                return title;
            }
        }
        public string IPAddress { get; set; }
        public string ComputerName { get; set; }
    }
}
