using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Controllers
{
    public class ControllerAndItsActions
    {
        public string Controller { get; private set; }
        public string? ControllerNameFa { get; private set; }
        public List<ActionName> Actions { get; private set; } = new List<ActionName>();

        public ControllerAndItsActions(string controller, List<ActionName> actions, string controllerNameFa)
        {
            Controller = controller;
            ControllerNameFa = controllerNameFa;
            Actions = actions;
        }
    }
    public class ActionName
    {
        public string NameFa { get; set; }
        public string NameEn { get; set; }
    }
}
