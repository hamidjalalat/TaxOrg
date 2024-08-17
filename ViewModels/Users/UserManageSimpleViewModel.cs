using Domain.Anemic.Entities;
using Domain.Enums;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UserManageSimpleViewModel:IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string OrganizationalUnit { get; set; }
        //public string FullName { get; set; }

		// ********************
		//[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		//public string FullName
		//{
		//	get
		//	{
		//		string strResult = $"{FirstName} {LastName}";

		//		return strResult;
		//	}
		//}

	}
}
