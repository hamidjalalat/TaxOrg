using Domain.Anemic.Entities;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Controllers;

namespace ViewModels.ActionMethods
{
    public class ActionMethodViewModel : IMapFrom<ActionMethod>
    {
        public int ActionMethodId { get; set; }
        public int ControllerId { get; set; }
        public ControllerViewModel? Controller { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
