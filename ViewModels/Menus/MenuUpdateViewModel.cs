using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Menus
{
    public class MenuUpdateViewModel:IMapFrom<Menu>
    {
        public int MenuId { get; set; }
        //public string Title { get; set; }
        public bool IsVisible { get; set; }
        //public string MenuIcon { get; set; }
        //public string PageIcon { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        //public string Url { get; set; }
        public int Sort { get; set; }
    }
}
