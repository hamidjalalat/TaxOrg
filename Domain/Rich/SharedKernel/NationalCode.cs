using System.Linq;

namespace Domain.Rich.SharedKernel
{
    public class NationalCode : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int FixLength = 10;

        public const string RegularExpression = "^[0-9]{10}$";
        #endregion /Constant(s)

        #region Static Member(s)
        #region ValidateNationalCode:
        public static bool ValidateNationalCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                return true;
            }

            try
            {
                var equalDigits = new[]
                {
                    "0000000000",
                    "1111111111",
                    "2222222222",
                    "3333333333",
                    "4444444444",
                    "5555555555",
                    "6666666666",
                    "7777777777",
                    "8888888888",
                    "9999999999"
                };

                if (equalDigits.Contains(value: value))
                {
                    return false;
                }

                var nationalCodeArray = value.ToCharArray();

                int firstNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[0].ToString()) * 10;

                int secondNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[1].ToString()) * 9;

                int thirdNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[2].ToString()) * 8;

                int fourthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[3].ToString()) * 7;

                int fifthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[4].ToString()) * 6;

                int sixthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[5].ToString()) * 5;

                int seventhNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[6].ToString()) * 4;

                int eighthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[7].ToString()) * 3;

                int ninthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[8].ToString()) * 2;

                int tenthNumber =
                    System.Convert.ToInt32(value: nationalCodeArray[9].ToString()) * 1;

                int sum = firstNumber + secondNumber + thirdNumber +
                    fourthNumber + fifthNumber + sixthNumber + seventhNumber + eighthNumber + ninthNumber;

                var result = sum % 11;

                if (result < 2 && tenthNumber == result || result >= 2 && 11 - result == tenthNumber)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion /ValidateNationalCode

        public static FluentResults.Result<NationalCode> Create(string value)
        {
            var result =
                new FluentResults.Result<NationalCode>();

            value =
                Dtat.String.Fix(text: value);

            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, Resources.DataDictionary.NationalCode);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.FixLengthNumeric,
                    Resources.DataDictionary.NationalCode, FixLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (ValidateNationalCode(value: value) is false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.InvalidValue,
                    Resources.DataDictionary.NationalCode);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            var returnValue =
                new NationalCode(value: value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        /// <summary>
        /// For EF Core!
        /// </summary>
        private NationalCode() : base()
        {
        }

        protected NationalCode(string value) : this()
        {
            Value = value;
        }

        public string Value { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
