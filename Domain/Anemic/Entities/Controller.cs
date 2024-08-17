using Domain.Anemic.Common;
using EntityFrameworkCore.AutoHistory.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Anemic.Entities
{
    [ExcludeFromHistory]
    public class Controller : Entity
    {
        public int ControllerId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
        public bool IsShow { get; set; }
        public List<ActionMethod>? ActionMethods { get; set; }
       
    }
}
