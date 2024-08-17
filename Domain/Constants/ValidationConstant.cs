namespace Domain.Constants
{
    public class ValidationConstant
    {
        public const string PasswordPattern = @"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{8,}$";
        public const string PersianNumberPattern = "[۰۱۲۳۴۵۶۷۸۹]+";
        public const string OnlyPersianCharactersPattern = "^[كي آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی]+$";
        public const string OnlyEnglishCharactersPattern = "^[A-Za-z]+$";
        public const string CellphonePattern = "^09[0-9]{9}$";
        public const string NationalCodePattern = "^(|-1|[0-9]{10})$";
        public const string NationalIdentityPattern = "^(|-1|[0-9]{11})$";
        public const string NationalCodeNationalIdentityPattern = @"^(|-1|[0-9]{10}|[0-9]{11})$";
        public const string CertificateSeriPattern = "^[0-9]{5,6}$";
        public const string CertificateSeriCharPattern = "^[كيآابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی]{1,6}$";
        public const string CertificateNoPattern = "^[0-9]{1,10}$";
        public const string NumericPattern = "^([0-9])*$";
        public const string UserNamePattern = "^([-]|[_]|[a-zA-Z0-9]|[.]|[@]|[$]|[%]|[!]){5,40}$";
        public const string PostCodePattern = "^([1-9][0-9]{9})$";
        public const string CertificateSeriNoPattern = "^[0-9]{0,2}$";

        public const string PhonePattern = "^0[1-9][0-9]{9}$";
        public const string EmailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9.-]+$";
        public const string WebSitePattern = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
        public const string SocialNetworkPattern = @"^[a-zA-Z0-9_.@-]+$";
        public const string FaxPattern = "^0[1-9][0-9]{9}$";

        public const string EnglishNumberPattern = "^[0-9]+$";
        public const string ValidNamePattern = "^[كي آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیA-Za-z]+$";
        public const string ValidCharactersPattern = "^[كي آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیA-Za-z0-9]+$";

        public const string ValidAddressPattern = "^[كي آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیA-Za-z0-9 ._-]+$";

        public const string PublicPattern = "^([a-zA-Z]|[ء-ٞ]|[پ]|[چ]|[ژ]|[ک]|[گ]|[ی]|[ۀ]|[?]|[ ]|[-]|[_]|[0-9]|[!]|[@]|[#]|[$]|[%]|[^]|[&]|[*]|[(]|[)]|[+]|[=]|[.]|[,]|[<]|[>]|[,]|[/]|[?]|[\\]|[|]|[{]|[}]|[[]|[]]|[٠]|[١]|[٢]|[٣]|[٤]|[٥]|[٦]|[٧]|[٨]|[٩]|[۰]|[۱]|[۲]|[۳]|[۴]|[۵]|[۶]|[۷]|[۸]|[۹]|[\r]|[\n]|[\t])*$";
        public const string ValidUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.@-";
        public const string ShabaPattern = "^(?:IR)(?=.{24}$)[0-9]*$";
        public const string Date = "([12]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]))";
    }
}
