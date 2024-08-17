
namespace ViewModels
{
    public class Utility
    {
        private static Utility _instance;
        public static Utility GetInstance()
        {
            if (_instance == null)
                _instance = new Utility();
            return _instance;
        }

        public string getIsActiveTitle(string? isFlag)
        {
            string strResult = string.Empty;
            if (isFlag != null)
                strResult = (isFlag.Trim().ToLower() == "true") ? Resources.DataDictionary.Active : Resources.DataDictionary.InActive;

            return strResult;
        }

        public string getYesNoTitle(string? isFlag)
        {
            string strResult = string.Empty;
            if (isFlag != null)
                strResult = (isFlag.Trim().ToLower() == "true") ? Resources.DataDictionary.Yes : Resources.DataDictionary.No;

            return strResult;
        }
    }
}
