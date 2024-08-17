namespace Domain.Rich.SharedKernel
{
    public class DateTime : SeedWork.ValueObject
    {
        #region Static Member(s)
        public static FluentResults.Result<DateTime> Create(System.DateTime? value)
        {
            var result =
                new FluentResults.Result<DateTime>();

            var returnValue =
                new DateTime(value: value.Value);

            result.WithValue(value: returnValue);

            return result;
        }

        public static bool operator <(DateTime left, DateTime right)
        {
            return left.Value < right.Value;
        }

        public static bool operator <=(DateTime left, DateTime right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >(DateTime left, DateTime right)
        {
            return left.Value > right.Value;
        }

        public static bool operator >=(DateTime left, DateTime right)
        {
            return left.Value >= right.Value;
        }
        #endregion /Static Member(s)

        /// <summary>
        /// For EF Core!
        /// باشد protected نکته: باید
        /// </summary>
        protected DateTime() : base()
        {
        }

        /// <summary>
        /// باشد protected نکته: باید
        /// </summary>
        protected DateTime(System.DateTime value) : this()
        {
            Value = value;
        }

        public System.DateTime Value { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            string result =
                Value.ToString("yyyy/MM/dd");

            return result;
        }
    }
}
