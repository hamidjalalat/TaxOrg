using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NazmMapping.Mappings;

namespace ViewModels.RoleManages
{
    public class RoleManageViewModel : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RoleManageViewModel, IdentityRole>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.RoleName))
                .ReverseMap();
        }
    }
}
