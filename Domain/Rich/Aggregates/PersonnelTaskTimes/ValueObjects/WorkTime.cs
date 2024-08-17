namespace Domain.Rich.Aggregates.PersonnelTaskTimes.ValueObjects
{
    public class WorkTime : SharedKernel.IntegerRange
    {
        #region Constant(s)
        public const int MinimumValue = 1;

        public const int MaximumValue = 1440;
        #endregion /Constant(s)

        #region Static Member(s)
        public static FluentResults.Result<WorkTime> Create(int? value)
        {
            var result =
                new FluentResults.Result<WorkTime>();

            // **************************************************
            var integerRangeResult =
                Create(value: value,
                minimumValue: MinimumValue, maximumValue: MaximumValue,
                caption: Resources.DataDictionary.WorkTime);

            result.WithErrors
                (errors: integerRangeResult.Errors);
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
                new WorkTime
                (value: integerRangeResult.Value.Value);

            result.WithValue
                (value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private WorkTime() : base()
        {
        }

        private WorkTime(int value) : base(value: value)
        {
        }

    }
}
