using Microsoft.AspNetCore.Mvc.Routing;
using Nazm.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ViewModels.Controllers;

namespace Api.Common
{
    public static class Settings
    {
        public static List<ControllerAndItsActions> GetAllControllersAndTheirActions()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            IEnumerable<Type> controllers = asm.GetTypes().Where(type => type.Name.EndsWith("Controller"));
            var theList = new List<ControllerAndItsActions>();

            foreach (Type curController in controllers)
            {
                var attr = curController.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().SingleOrDefault();
                string displayName = attr?.Name;
                var actions = curController.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .Where(m => m.CustomAttributes.Any(a => typeof(HttpMethodAttribute).IsAssignableFrom(a.AttributeType)))
                    .ToList();

                var actionList = new List<ActionName>();
                foreach (var item in actions)
                {
                    var attribute = item.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().SingleOrDefault();
                    string displayNameFa = attribute?.Name;
                    actionList.Add(new ActionName { NameEn = item.Name, NameFa = displayNameFa });
                }
                theList.Add(new ControllerAndItsActions(curController.Name, actionList, displayName));
            }
            return theList;
        }
    }
}
