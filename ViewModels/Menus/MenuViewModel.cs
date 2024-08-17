using AutoMapper;
using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using ViewModels.Controllers;

namespace ViewModels.Menus

{
    public class MenuViewModel : IMapFrom<Menu>
    {
        public MenuViewModel() 
        {
        }
        public int MenuId { get; set; }
        public string Title { get; set; }
        //public HierarchyId ParentId { get; set; }
        public string ParentIdString { get; set; }
        public short ParentLevel { get; set; }
        public bool IsVisible { get; set; }
        public string IsVisibleTitle
        {
            get { return Utility.GetInstance().getIsActiveTitle(IsVisible.ToString()); }
        }
        public string MenuIcon { get; set; }
        public string PageIcon { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
		public string EntityName { get; set; }

		public void Mapping(Profile profile)
        {
            profile.CreateMap<Menu, MenuViewModel>()
                .ForMember(d => d.ParentIdString, opt => opt.MapFrom(s => s.ParentId.ToString()))
                .ForMember(d => d.ParentLevel, opt => opt.MapFrom(s => s.ParentId.GetLevel()))
            .ReverseMap();
        }

    }
}
