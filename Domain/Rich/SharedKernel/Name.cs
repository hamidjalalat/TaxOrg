namespace Domain.Rich.SharedKernel
{
    public class Name : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int MaxLength = 50;
        #endregion /Constant(s)

        #region Static Member(s)

        #region Create
        internal static
            FluentResults.Result<Name>
            Create(string value, string caption = null, int? maxLength = null)
        {
            var result =
                new FluentResults.Result<Name>();

            // **************************************************
            if (string.IsNullOrWhiteSpace(caption))
            {
                caption =
                    Resources.DataDictionary.Name;
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
                new Name(value: value);

            result.WithValue(value: resultValue);

            return result;
        }
        #endregion /Create

        #endregion /Static Member(s)

        protected Name() : base()
        {
        }

        protected Name(string value) : this()
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
