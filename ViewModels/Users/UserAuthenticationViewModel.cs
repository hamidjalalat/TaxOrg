using Domain.Anemic.Entities;
using Domain.Enums;
using NazmMapping.Mappings;
using Resources;

namespace ViewModels.Users
{
    public class UserAuthenticationViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string OrganizationalUnit { get; set; }
        public string UserName { get; set; }

        // ********************
        public string getGender
        {
            get
            {
                string strResult = (Gender == GenderTypeEnum.Male) ? DataDictionary.Male_2 : DataDictionary.Female;
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
                string strResult = $"{getGender} {FullName}";
                return strResult;
            }
        }

    }
}
