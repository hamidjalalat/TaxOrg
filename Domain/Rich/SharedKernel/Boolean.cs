namespace Domain.Rich.SharedKernel
{
    public class Boolean : SeedWork.ValueObject
    {
        #region Static Member(s)
        public static FluentResults.Result<Boolean> Create(bool? value)
        {
            var result =
                new FluentResults.Result<Boolean>();

            // **************************************************
            if (value is null)
            {
                value = false;
            }
            // **************************************************

            var returnValue =
                new Boolean(value: value.Value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private Boolean() : base()
        {
        }

        private Boolean(bool value) : this()
        {
            Value = value;
        }

        public bool Value { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        internal Boolean Activate()
        {
            if (Value)
            {
                return this;
            }
            else
            {
                var result =
                    Create(value: true);

                return result.Value;
            }
        }

        internal Boolean Deactivate()
        {
            if (Value == false)
            {
                return this;
            }
            else
            {
                var result =
                    Create(value: false);

                return result.Value;
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
