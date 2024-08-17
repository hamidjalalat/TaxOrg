using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Anemic.Common;
using Domain.Enums;

namespace Domain.Anemic.Entities
{
    public class LoginHistory: Entity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime LogDate { get; set; }
        public HistoryTypeEnum HistoryType { get; set; }
        public string IPAddress { get; set; }
        public string ComputerName { get; set; }
    }
}
