using System.Linq;
using Domain.Rich.Aggregates.PersonnelProjects;
using Domain.Rich.SeedWork;

namespace Domain.Rich.Aggregates.Personnels
{
    public class Personnel : AggregateRoot
    {
        public static FluentResults.Result<Personnel> Create(string firstName, string lastName)
        {

            var result = new FluentResults.Result<Personnel>();

            var firstNameResult = SharedKernel.FirstName.Create(firstName);

            result.WithErrors(errors: firstNameResult.Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var lastNameResult = SharedKernel.LastName.Create(lastName);

            result.WithErrors(errors: lastNameResult.Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var resultValue = new Personnel(firstNameResult.Value, lastNameResult.Value);

            result.WithValue(value: resultValue);

            return result;
        }

        private Personnel() : base()
        {

            _personnelProjects =
                new System.Collections.Generic.List<PersonnelProject>();

            _personnelTaskTimes =
                new System.Collections.Generic.List<PersonnelTaskTimes.PersonnelTaskTime>();

        }

        private Personnel(SharedKernel.FirstName firstName, SharedKernel.LastName lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public SharedKernel.FirstName FirstName { get; private set; }

        public SharedKernel.LastName LastName { get; private set; }

        //public virtual Projects.Project Project { get; private set; }


        // **********
        private readonly System.Collections.Generic.List<PersonnelProject> _personnelProjects;

        public virtual System.Collections.Generic.IReadOnlyList<PersonnelProject> PersonnelProjects
        {
            get
            {
                return _personnelProjects;
            }
        }
        // **********

        // **********
        private readonly System.Collections.Generic.List<PersonnelTaskTimes.PersonnelTaskTime> _personnelTaskTimes;

        public virtual System.Collections.Generic.IReadOnlyList<PersonnelTaskTimes.PersonnelTaskTime> PersonnelTaskTimes
        {
            get
            {
                return _personnelTaskTimes;
            }
        }
        // **********

        public FluentResults.Result Update(string firstName, string lastName)
        {

            var result = Create(firstName: firstName, lastName: lastName);

            result.WithErrors(errors: result.Errors);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            FirstName = result.Value.FirstName;
            LastName = result.Value.LastName;

            return result.ToResult();

        }


        #region Add Personnel Project
        public
            FluentResults.Result
            <PersonnelProject>
            AddPersonnelProject(Projects.Project project)
        {
            var result =
                new FluentResults.Result
                <PersonnelProject>();

            // **************************************************
            var personnelProjectResult =
                PersonnelProject.Create
                (personnel: this, project: project);

            if (personnelProjectResult.IsFailed)
            {
                result.WithErrors(errors: personnelProjectResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            // **************************************************
            var hasAny =
                _personnelProjects
                .Where(current => current.Personnel.Id
                    == personnelProjectResult.Value.Id)
                .Any();

            if (hasAny)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.PersonnelProject); //PersonnelProjectName

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _personnelProjects.Add(item: personnelProjectResult.Value);

            result.WithValue(value: personnelProjectResult.Value);

            return result;
        }
        #endregion /Add Personnel Project

        #region Remove Personnel Project
        public
            FluentResults.Result
            RemovePersonnelProject
            (Projects.Project project)
        {
            var result =
                new FluentResults.Result();

            // **************************************************
            var personnelProjectResult =
                PersonnelProject.Create
                (personnel: this, project: project);

            if (personnelProjectResult.IsFailed)
            {
                result.WithErrors(errors: personnelProjectResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            // **************************************************
            var foundedPersonnelProject =
                _personnelProjects
                .Where(current => current.Personnel.Id
                    == personnelProjectResult.Value.Personnel.Id
                    &&
                    current.Project.Id
                    == personnelProjectResult.Value.Project.Id
                    )
                .FirstOrDefault();

            if (foundedPersonnelProject is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.PersonnelProject);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _personnelProjects.Remove(item: foundedPersonnelProject);

            return result;
        }
        #endregion /Remove Personnel Project

        #region Add Personnel TaskTime
        public
            FluentResults.Result
            <PersonnelTaskTimes.PersonnelTaskTime>
            AddPersonnelTaskTime(Tasks.Task task, System.DateTime? workDate, int? workTime, string descript)
        {
            var result =
                new FluentResults.Result
                <PersonnelTaskTimes.PersonnelTaskTime>();

            // **************************************************
            var personnelTaskTimeResult =
                Aggregates.PersonnelTaskTimes.PersonnelTaskTime.Create
                (personnel: this, task: task, workDate: workDate, workTime: workTime, descript: descript);

            if (personnelTaskTimeResult.IsFailed)
            {
                result.WithErrors(errors: personnelTaskTimeResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            // **************************************************
            /*
            var hasAny =
                _personnelTaskTimes
                .Where(current => current.Personnel.Id
                    == personnelTaskTimeResult.Value.Id)
                .Any();

            if (hasAny)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.PersonnelProject); //PersonnelProjectName

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            */
            // **************************************************

            _personnelTaskTimes.Add(item: personnelTaskTimeResult.Value);

            result.WithValue(value: personnelTaskTimeResult.Value);

            return result;
        }
        #endregion /Add Personnel TaskTime

        #region Remove Personnel TaskTime
        public
            FluentResults.Result
            RemovePersonnelTaskTime
            (Tasks.Task task, System.DateTime? workDate, int? workTime, string descript)
        {
            var result =
                new FluentResults.Result();

            // **************************************************
            var personnelTaskTimeResult =
                Aggregates.PersonnelTaskTimes.PersonnelTaskTime.Create
                (personnel: this, task: task, workDate: workDate, workTime: workTime, descript: descript);

            if (personnelTaskTimeResult.IsFailed)
            {
                result.WithErrors(errors: personnelTaskTimeResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            // **************************************************
            var foundedPersonnelTaskTime =
                _personnelTaskTimes
                .Where(current => current.Personnel.Id
                    == personnelTaskTimeResult.Value.Personnel.Id
                    &&
                    current.Task.Id
                    == personnelTaskTimeResult.Value.Task.Id
                    &&
                    current.WorkDate
                    == personnelTaskTimeResult.Value.WorkDate
                    &&
                    current.WorkTime
                    == personnelTaskTimeResult.Value.WorkTime
                    &&
                    current.Descript
                    == personnelTaskTimeResult.Value.Descript
                    )
                .FirstOrDefault();

            if (foundedPersonnelTaskTime is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.PersonnelTaskTime);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _personnelTaskTimes.Remove(item: foundedPersonnelTaskTime);

            return result;
        }
        #endregion /Remove Personnel Project

    }

}
