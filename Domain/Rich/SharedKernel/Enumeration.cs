namespace Domain.Rich.SharedKernel
{
    public class Enumeration : SeedWork.Enumeration
    {
        #region Constant(s)
        #endregion /Constant(s)

        #region Static Member(s)
        internal
            static
            FluentResults.Result<TValue> GetByValue<TValue>
            (int? value, string name) where TValue : SeedWork.Enumeration
        {
            var result =
                new FluentResults.Result<TValue>();

            // **************************************************
            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, name);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var enumValue =
                FromValue<TValue>(value: value.Value);

            if (enumValue is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.InvalidCode, name);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            result.WithValue(value: enumValue);

            return result;
        }
        #endregion /Static Member(s)

        protected Enumeration() : base()
        {
        }

        protected Enumeration(int value, string name) : base(value: value, name: name)
        {
        }
    }
}
