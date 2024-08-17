using Domain.Anemic.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Nazm.Extensions;
using NazmMapping.Mappings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using ViewModels.Users;

namespace ViewModels.Histories
{
    public class HistoryViewModel : IMapFrom<EntityAutoHistory>
    {
        public long Id { get; set; }
		public string UserFirstName { get; set; }
		public string UserLastName { get; set; }
		public string IPAddress { get; set; }
        public string ComputerName { get; set; }
        public HistoryActionTypeEnum? HistoryActionType { get; set; }
        public string HistoryActionTypeTitle
        {
            get
            {
                string title = string.Empty;
                switch (HistoryActionType)
                {
                    case Domain.Enums.HistoryActionTypeEnum.View:
                        title = Buttons.View;
                        break;
                    case Domain.Enums.HistoryActionTypeEnum.Confirm1:
                        title = Buttons.Confirm1;
                        break;
                    case Domain.Enums.HistoryActionTypeEnum.Confirm2:
                        title = Buttons.Confirm2;
                        break;
                    case Domain.Enums.HistoryActionTypeEnum.Reject1:
                        title = Buttons.Reject1;
                        break;
                    case Domain.Enums.HistoryActionTypeEnum.Reject2:
                        title = Buttons.Reject2;
                        break;
                    case Domain.Enums.HistoryActionTypeEnum.Archive:
                        title = Buttons.Archive;
                        break;
                    default:
                        title = "---";
                        break;
                }
                return title;
            }
        }
        public bool IsDeleted { get; set; }
        public string TableName { get; set; }
        private string _tableNameFa;

        public string TableNameFa
        {
            get
            {
                return TableName.GetTableName();
            }

        }



        public EntityState Kind { get; set; }
        public string KindTitle
        {
            get
            {
                string title = string.Empty;
                switch (Kind)
                {
                    case EntityState.Deleted:
                        title = "حذف ";
                        break;
                    case EntityState.Modified:
                        if (IsDeleted) title = "حذف";
                        else
                            title = "ویرایش ";
                        break;
                    case EntityState.Added:
                        title = "درج";
                        break;
                    default:
                        title = "---";
                        break;
                }
                return title;
            }
        }
        public DateTime CreateDate { get; set; }
        public string CreateDateSH
        {
            get
            {
                return CreateDate.ToPersianDateTime();
            }
        }
    }
}
