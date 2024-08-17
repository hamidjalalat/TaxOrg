using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MenuControllerActions
{
    public class MenuControllerActionViewModel
    {
        public int MenuControllerActionId { get; set; }
        public int MenuControllerId { get; set; }
        public MenuController MenuController { get; set; }
        public int ActionMethodId { get; set; }
        public ActionMethod ActionMethod { get; set; }
    }
}
