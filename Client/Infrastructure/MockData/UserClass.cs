using Resources;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.MockData
{
    public class UserClass
    {
        public UserClass()
        {
        }

        // ********************
        //public int Id { get; set; }
        public string Id { get; set; }

        // ********************
        public string FirstName { get; set; }

        // ********************
        public string LastName { get; set; }

        // ********************
        public string OrganizationalUnit { get; set; }

        // ********************
        public Domain.Enums.GenderTypeEnum Gender { get; set; }

        // ********************
        [Display(
            Name = nameof(DataDictionary.UserName),
            ResourceType = typeof(DataDictionary)
        )]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required)
        )]
        public string UserName { get; set; }

        // ********************
        [Display(
            Name = nameof(DataDictionary.Password),
            ResourceType = typeof(DataDictionary)
        )]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required)
        )]
        public string Password { get; set; }



		// ********************
        /// <summary>
        /// آقا/خانم
        /// </summary>
		public string GenderTitle1
        {
            get
            {
                string strResult = (Gender == Domain.Enums.GenderTypeEnum.Male) ? 
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
                string strResult = (Gender == Domain.Enums.GenderTypeEnum.Male) ? 
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
                string strResult = (Gender == Domain.Enums.GenderTypeEnum.Male) ? 
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
                string strResult = $"{ FirstName } { LastName }";
                return strResult;
            }
        }

		// ********************
		public string FullNameGender
        {
            get
            {
                string strResult = $"{ GenderTitle2 } { FullName }";
                return strResult;
            }
        }

    }
}
