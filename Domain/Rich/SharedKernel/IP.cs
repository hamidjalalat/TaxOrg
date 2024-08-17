namespace Domain.Rich.SharedKernel
{
    public class IP : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int MaxLength = 15;

        public const string RegularExpression = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)(\.(?!$)|$)){4}$";
        #endregion /Constant(s)

        #region Static Member(s)
        internal static
            FluentResults.Result<IP>
            Create(string value, string caption = null, int? maxLength = null)
        {
            var result =
                new FluentResults.Result<IP>();

            // **************************************************
            if (string.IsNullOrWhiteSpace(caption))
            {
                caption =
                    Resources.DataDictionary.IP;
            }
            // **************************************************

            // **************************************************
            if (maxLength is null)
            {
                maxLength = MaxLength;
            }
            // **************************************************

            value =
                Dtat.String.Fix(text: value);

            // **************************************************
            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, caption);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            if (value.Length > maxLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.MaxLength, caption, maxLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            if (System.Text.RegularExpressions.Regex.IsMatch
                (input: value, pattern: RegularExpression) is false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.RegularExpression, caption);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var resultValue =
                new IP(value: value);

            result.WithValue(value: resultValue);

            return result;
        }
        #endregion /Static Member(s)

        protected IP() : base()
        {
        }

        protected IP(string value) : this()
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
