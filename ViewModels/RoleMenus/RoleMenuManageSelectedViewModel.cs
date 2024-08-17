using AutoMapper;
using Domain.Rich.Aggregates.Menus;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.RoleMenus
{
    public class RoleMenuManageSelectedViewModel:IMapFrom<Domain.Anemic.Entities.Menu>
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string ParentIdString { get; set; }
        public short ParentLevel { get; set; }
        public bool IsSelected { get; set; }
        public string Url { get; set; }
		public void Mapping(Profile profile)
        {
			profile.CreateMap<Domain.Anemic.Entities.Menu, RoleMenuManageSelectedViewModel>()
				.ForMember(d => d.MenuId, opt => opt.MapFrom(s => s.MenuId))
				.ForMember(d => d.MenuTitle, opt => opt.MapFrom(s => s.Title))
				.ForMember(d => d.ParentIdString, opt => opt.MapFrom(s => s.ParentId.ToString()))
				.ForMember(d => d.ParentLevel, opt => opt.MapFrom(s => s.ParentId.GetLevel()))
				.ForMember(d => d.Url, opt => opt.MapFrom(s => s.Url))
				.ReverseMap();
		}
	}
}
