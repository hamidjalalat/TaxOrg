namespace Domain.Rich.SharedKernel
{
    public class IntegerRange : SeedWork.ValueObject
    {
        #region Static Member(s)
        public static FluentResults.Result<IntegerRange>
            Create(int? value, int minimumValue, int maximumValue, string caption)
        {
            var result =
                new FluentResults.Result<IntegerRange>();

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
            if (value.Value < minimumValue || value.Value > maximumValue)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Range,
                    caption, minimumValue, maximumValue);

                result.WithError
                    (errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var returnValue =
                new IntegerRange(value: value.Value);

            result.WithValue
                (value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        protected IntegerRange() : base()
        {
        }

        protected IntegerRange(int value) : this()
        {
            Value = value;
        }

        public int Value { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
