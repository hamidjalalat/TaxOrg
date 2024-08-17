using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Controllers;

namespace ViewModels.MenuControllers
{
    public class MenuControllerViewModel
    {
        public int MenuControllerId { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int ControllerId { get; set; }
        public ControllerViewModel Controller { get; set; }
    }
}
