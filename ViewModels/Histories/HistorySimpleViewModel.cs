using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Histories
{
    public class HistorySimpleViewModel
    {
        public string Changed { get; set; }
        public EntityState Kind { get; set; }
		public HistoryActionTypeEnum? HistoryActionType { get; set; }
		public bool IsDeleted { get; set; }
    }
}
