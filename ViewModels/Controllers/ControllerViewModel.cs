using Domain.Anemic.Entities;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Controllers
{
    public class ControllerViewModel : IMapFrom<Controller>
    {
        public int ControllerId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
