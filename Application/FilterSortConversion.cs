
namespace Application
{
    public class FilterSortConversion
    {
        #region Static Methods

        private static FilterSortConversion _instance;
        public static FilterSortConversion GetInstance()
        {
            if (_instance == null)
                _instance = new FilterSortConversion();
            return _instance;
        }

        #endregion
        public FilterSortConversion()
        {

        }

        public string FilterSortToWhereOrderInDapper(string oper, object value)
        {
            string strResult = null;
            var type = value.GetType();
            var condition = (type == typeof(string));

            switch (oper.ToLower())
            {
                case "equals":
                    //strResult = " = " + ((condition) ? string.Format("'{0}'", value) : value); 
                    strResult = $" = " + ((condition) ? $"'{value}'" : value);
                    break;
                case "notequal":
                    strResult = $" <> " + ((condition) ? $"'{value}'" : value);
                    break;
                case "contains":
                    strResult = $" LIKE " + $"N'%{value}%'";
                    break;
                case "doesnotcontain":
                    strResult = $" Not LIKE " + $"N'%{value}%'";
                    break;
                case "greaterthan":
                    strResult = $" > {value}";
                    break;
                case "greaterthanorequals":
                    strResult = $" >= {value}";
                    break;
                case "lessthan":
                    strResult = $" < {value}";
                    break;
                case "lessthanorequals":
                    strResult = $" <= {value}";
                    break;
                case "startswith":
                    strResult = $" LIKE " + $"N'{value}%'";
                    break;
                case "endswith":
                    strResult = $" LIKE " + $"N'%{value}'";
                    break;
                case "isnull":
                    strResult = $" IS NULL " + ((condition) ? $"'{value}'" : value);
                    break;
                case "isnotnull":
                    strResult = $" IS NOT NULL " + ((condition) ? $"'{value}'" : value);
                    break;
                default:
                    strResult = $" = " + ((condition) ? $"'{value}'" : value);
                    break;
            }

            return strResult;
        }
    }
}
