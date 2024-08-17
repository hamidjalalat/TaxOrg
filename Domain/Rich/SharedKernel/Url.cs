using Domain.Rich.SeedWork;

namespace Domain.Rich.SharedKernel
{
    public class Url : ValueObject
    {
        #region Constant(s)
        public const int MaxLength = 200;
        #endregion /Constant(s)

        #region Static Member(s)
        internal static
            FluentResults.Result<Url>
            Create(string value, string caption = null, int? maxLength = null)
        {
            var result =
                new FluentResults.Result<Url>();

            // **************************************************
            if (string.IsNullOrWhiteSpace(value: caption))
            {
                caption =
                    Resources.DataDictionary.Url;
            }
            // **************************************************

            // **************************************************
            if (maxLength is null)
            {
                maxLength = MaxLength;
            }
            // **************************************************

            // **************************************************
            value =
                Dtat.String.Fix(text: value);
            // **************************************************

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
            if (System.Uri.IsWellFormedUriString
                (uriString: value, System.UriKind.Absolute) is false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.InvalidValue, caption);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var resultValue =
                new Url(value: value);

            result.WithValue(value: resultValue);

            return result;
        }
        #endregion /Static Member(s)

        protected Url() : base()
        {
        }

        protected Url(string value) : this()
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
