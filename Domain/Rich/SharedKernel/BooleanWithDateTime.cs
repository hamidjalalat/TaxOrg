using Domain.Rich.SeedWork;

namespace Domain.Rich.SharedKernel
{
    public class BooleanWithDateTime : ValueObject
    {
        #region Static Member(s)
        public static FluentResults.Result<BooleanWithDateTime>
            Create(bool? value, System.DateTime? timestamp = null)
        {
            var result =
                new FluentResults.Result<BooleanWithDateTime>();

            // **************************************************
            if (value is null)
            {
                value = false;
            }
            // **************************************************

            if (timestamp == null)
            {
                timestamp =
                    Utility.Now;
            }

            var timestampResult =
                DateTime.Create(value: timestamp).Value;

            var returnValue =
                new BooleanWithDateTime
                (value: value.Value, timestamp: timestampResult);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private BooleanWithDateTime() : base()
        {
        }

        private BooleanWithDateTime(bool value, DateTime timestamp) : this()
        {
            Value = value;
            Timestamp = timestamp;
        }

        public bool Value { get; }

        public DateTime Timestamp { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Timestamp;
        }

        internal BooleanWithDateTime Activate()
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

        internal BooleanWithDateTime Deactivate()
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
            string result =
                $"{Value} - {Timestamp:MM/dd/yyyy HH:mm:ss}";

            return result;
        }
    }
}
