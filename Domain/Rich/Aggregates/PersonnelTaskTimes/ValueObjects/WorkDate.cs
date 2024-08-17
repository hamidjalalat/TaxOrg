using Domain.Rich.SharedKernel;

namespace Domain.Rich.Aggregates.PersonnelTaskTimes.ValueObjects
{
    public class WorkDate : DateTime
    {
        #region Constant(s)
        #endregion /Constant(s)

        #region Static Member(s)
        public new static FluentResults.Result<WorkDate> Create(System.DateTime? value)
        {
            var result =
                new FluentResults.Result<WorkDate>();

            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.WorkDate);

                result.WithError(errorMessage: errorMessage);
            }

            System.DateTime MinDateValid = new System.DateTime(2022, 03, 21);

            if (value > System.DateTime.Today.AddDays(1).Date || value < MinDateValid)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Range, //مقدار فیلد {0} باید بزرگ‌تر یا مساوی {1} و کوچک‌تر یا مساوی {2} باشد.
                    Resources.DataDictionary.WorkDate, "1401/01/01", "امروز");

                result.WithError(errorMessage: errorMessage);
            }

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
                new WorkDate(value: value.Value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private WorkDate() : base()
        {
        }

        private WorkDate(System.DateTime value) : base(value: value)
        {
        }

    }
}
