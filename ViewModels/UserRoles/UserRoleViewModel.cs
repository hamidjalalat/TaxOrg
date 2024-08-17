using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.RoleManages;

namespace ViewModels.UserRoles
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            UserRoles = new List<RoleManageViewModel>();
        }

        public string UserId { get; set; }
        public List<RoleManageViewModel> UserRoles { get; set; }
    }
}
