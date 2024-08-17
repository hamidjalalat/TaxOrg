using Domain.Anemic.Entities;
using Domain.Enums;
using NazmMapping.Mappings;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Users
{
    public class UserManageUpdateViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

		
		public string FirstName { get; set; }

		
		public string LastName { get; set; }

		public string NationalCode { get; set; }

		public GenderTypeEnum Gender { get; set; }

		
		public string Email { get; set; }

		
		public string UserName { get; set; }

		
	}
}
