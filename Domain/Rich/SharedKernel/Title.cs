namespace Domain.Rich.SharedKernel
{
    public class Title : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int MaxLength = 200;
        #endregion /Constant(s)

        #region Static Member(s)
        internal static
            FluentResults.Result<Title>
            Create(string value, string caption = null, int? maxLength = null)
        {
            var result =
                new FluentResults.Result<Title>();

            // **************************************************
            if (string.IsNullOrWhiteSpace(value: caption))
            {
                caption =
                    Resources.DataDictionary.Title;
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

            var resultValue =
                new Title(value: value);

            result.WithValue(value: resultValue);

            return result;
        }
        #endregion /Static Member(s)

        protected Title() : base()
        {
        }

        protected Title(string value) : this()
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
