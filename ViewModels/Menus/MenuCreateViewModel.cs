using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;

namespace ViewModels.Menus
{
    public class MenuCreateViewModel 
    {

        public MenuCreateViewModel() : base()
        {
        }

        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int? ParentId { get; set; }
        public string MenuIcon { get; set; }
        public string PageIcon { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
		public string EntityName { get; set; }

	}
}
