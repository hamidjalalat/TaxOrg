namespace Domain.Rich.SharedKernel
{
    public class Text : SeedWork.ValueObject
    {
        #region Static Member(s)
        public static FluentResults.Result<Text>
            Create(string value, int minimumLength, int maximumLength, string caption)
        {
            var result =
                new FluentResults.Result<Text>();

            value =
                Dtat.String.Fix(text: value);

            // **************************************************
            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, caption);

                result.WithError
                    (errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            if (minimumLength > 0)
            {
                if (value.Length < minimumLength)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.MinLength, caption, minimumLength);

                    result.WithError
                        (errorMessage: errorMessage);

                    return result;
                }
            }
            // **************************************************

            // **************************************************
            if (minimumLength > 0)
            {
                if (value.Length > maximumLength)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.MaxLength, caption, minimumLength);

                    result.WithError
                        (errorMessage: errorMessage);

                    return result;
                }
            }
            // **************************************************

            var returnValue =
                new Text(value: value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        protected Text() : base()
        {
        }

        protected Text(string value) : this()
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
