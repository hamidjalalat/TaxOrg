using System.Linq;
using Domain.Rich.Aggregates.Personnels;
using Domain.Rich.Aggregates.PersonnelTaskTimes.ValueObjects;
using Domain.Rich.SeedWork;

namespace Domain.Rich.Aggregates.PersonnelTaskTimes
{
    public class PersonnelTaskTime : AggregateRoot
    {
        public static FluentResults.Result<PersonnelTaskTime> Create(
                                                                        Personnel personnel,
                                                                        Tasks.Task task,
                                                                        System.DateTime? workDate,
                                                                        int? workTime,
                                                                        string descript
                                                                    )
        {
            var result = new FluentResults.Result<PersonnelTaskTime>();

            // **************************************************
            if (personnel is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.Personnel);

                result.WithError(errorMessage: errorMessage);
            }
            // **************************************************

            // **************************************************
            if (task is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.Task);

                result.WithError(errorMessage: errorMessage);
            }
            // **************************************************

            // **************************************************
            if (workDate is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.WorkDate);

                result.WithError(errorMessage: errorMessage);
            }

            //var workDateResult = SharedKernel.DateTime.Create(workDate);
            var workDateResult = WorkDate.Create(workDate);

            result.WithErrors(errors: workDateResult.Errors);

            // **************************************************

            // **************************************************
            if (workTime is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.WorkTime);

                result.WithError(errorMessage: errorMessage);
            }

            //var workTimeResult = SharedKernel.IntegerRange.Create(workTime,1,1440,Resources.DataDictionary.WorkTime);
            var workTimeResult = WorkTime.Create(workTime);

            result.WithErrors(errors: workTimeResult.Errors);

            // **************************************************

            descript =
                Dtat.String.Fix(text: descript);

            // **************************************************
            if (result.IsFailed)
            {
                return result;
            }
            // **************************************************


            var resultValue = new PersonnelTaskTime(
                                                        personnel,
                                                        task,
                                                        workDateResult.Value,
                                                        workTimeResult.Value,
                                                        descript
                                                   );

            result.WithValue(value: resultValue);

            return result;
        }

        private PersonnelTaskTime() : base()
        {

        }

        private PersonnelTaskTime(
                                    Personnel personnel,
                                    Tasks.Task task,
                                    WorkDate workDate,
                                    WorkTime workTime,
                                    string descript
                                 ) : this()
        {
            Personnel = personnel;
            Task = task;
            WorkDate = workDate;
            WorkTime = workTime;
            Descript = descript;

        }

        public Personnel Personnel { get; private set; }

        public Tasks.Task Task { get; private set; }

        public WorkDate WorkDate;

        public WorkTime WorkTime;

        public string Descript { get; private set; }


        public FluentResults.Result Update(
                                                Personnel personnel,
                                                Tasks.Task task,
                                                System.DateTime? workDate,
                                                int? workTime,
                                                string descript
                                            )
        {
            var result = Create(
                                    personnel: personnel,
                                    task: task,
                                    workDate: workDate,
                                    workTime: workTime,
                                    descript: descript
                               );

            result.WithErrors(errors: result.Errors);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Personnel = result.Value.Personnel;
            Task = result.Value.Task;
            WorkDate = result.Value.WorkDate;
            WorkTime = result.Value.WorkTime;
            Descript = result.Value.Descript;

            return result.ToResult();

        }


    }
}
