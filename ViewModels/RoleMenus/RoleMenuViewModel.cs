using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using ViewModels.Roles;

namespace ViewModels.RoleMenus
{
    public class RoleMenuViewModel:IMapFrom<Claim>
    {
        public int MenuId { get; set; }
        public string RoleId { get; set; }
		//public string Type { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Claim, RoleMenuViewModel>()
				.ForMember(d => d.MenuId, opt => opt.MapFrom(s => s.Type))
				
				.ReverseMap();
		}
	}
}
