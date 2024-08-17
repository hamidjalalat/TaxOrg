using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Pluralize.NET.Core;
namespace Resources
{
    public static class Extension
    {
        public static string GetTableName(this string name)
        {
            var singular = new Pluralizer().Singularize(name);
            ResourceManager ResManager = new ResourceManager("Resources.DataDictionary", Assembly.GetExecutingAssembly());
            String strResourveValue = ResManager.GetString(singular);
            return strResourveValue; 
        }
        public static string GetFolderName(this string name)
        {
            return new Pluralizer().Pluralize(name);
        }
    }
}
