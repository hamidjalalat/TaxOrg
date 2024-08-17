
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UserManageCreateViewModel
    {
        
		public string FirstName { get; set; }

		
		public string LastName { get; set; }

		public string NationalCode { get; set; }

		public GenderTypeEnum Gender { get; set; }

		
        public string Email { get; set; }

        
        public string UserName { get; set; }

      
        public string Password { get; set; }

        
        public string ConfirmPassword { get; set; }
    }
}
