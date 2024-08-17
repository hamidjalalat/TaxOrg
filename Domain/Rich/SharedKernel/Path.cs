namespace Domain.Rich.SharedKernel
{
    public class Path : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int MaxLength = 200;
        #endregion /Constant(s)

        #region Static Member(s)
        internal static
            FluentResults.Result<Path>
            Create(string value, string caption = null, int? maxLength = null)
        {
            var result =
                new FluentResults.Result<Path>();

            // **************************************************
            if (string.IsNullOrWhiteSpace(value: caption))
            {
                caption =
                    Resources.DataDictionary.Path;
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
                (uriString: value, System.UriKind.Relative) is false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.InvalidValue, caption);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var returnValue =
                new Path(value: value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        protected Path() : base()
        {
        }

        protected Path(string value) : this()
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
