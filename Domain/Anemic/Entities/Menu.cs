using Domain.Anemic.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Anemic.Entities
{
    public class Menu : Entity
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
       
        public HierarchyId ParentId { get; set; }
        public bool IsVisible { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string PageIcon { get; set; }
        public string MenuIcon { get; set; }
        public int Sort { get; set; }
        public string EntityName { get; set; }

    }
}
