using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UserAuthenticationResetPasswordViewModel
    {
        
        public string NewPassword { get; set; }

       
        public string ConfirmNewPassword { get; set; }

       
        public string Token { get; set; }

       
        public string Email { get; set; }
    }
}
