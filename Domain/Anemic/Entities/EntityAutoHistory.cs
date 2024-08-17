
using Domain.Enums;
using EntityFrameworkCore.AutoHistory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Anemic.Entities
{
    public class EntityAutoHistory : AutoHistory
    {
        public string UserId { get; set; }
        public User? User { get; set; }
        public string IPAddress { get; set; }
        public string ComputerName { get; set; }
        public HistoryActionTypeEnum? HistoryActionType { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
