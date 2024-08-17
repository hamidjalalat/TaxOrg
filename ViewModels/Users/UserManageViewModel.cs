using Domain.Anemic.Entities;
using Domain.Enums;
using NazmMapping.Mappings;
using Resources;
using System;

namespace ViewModels.Users
{
    public class UserManageViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string OrganizationalUnit { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

		// ********************
		/// <summary>
		/// آقا/خانم
		/// </summary>
		public string GenderTitle1
		{
			get
			{
				string strResult = (Gender == GenderTypeEnum.Male) ?
					DataDictionary.Male :
					DataDictionary.Female;
				return strResult;
			}
		}

		/// <summary>
		/// آقای/خانم
		/// </summary>
		public string GenderTitle2
		{
			get
			{
				string strResult = (Gender == GenderTypeEnum.Male) ?
					DataDictionary.Male_2 :
					DataDictionary.Female;
				return strResult;
			}
		}

		/// <summary>
		/// مرد/زن
		/// </summary>
		public string GenderTitle3
		{
			get
			{
				string strResult = (Gender == GenderTypeEnum.Male) ?
					DataDictionary.Man :
					DataDictionary.Woman;
				return strResult;
			}
		}

		// ********************
		public string FullName
		{
			get
			{
				string strResult = $"{FirstName} {LastName}";
				return strResult;
			}
		}

		// ********************
		public string FullNameGender
		{
			get
			{
				string strResult = $"{GenderTitle2} {FullName}";
				return strResult;
			}
		}
	}
}
