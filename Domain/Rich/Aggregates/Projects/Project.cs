using System.Linq;
using Domain.Rich.Aggregates.PersonnelProjects;
using Domain.Rich.Aggregates.Projects.ValueObjects;
using Domain.Rich.Aggregates.Tasks;
using Domain.Rich.SeedWork;

namespace Domain.Rich.Aggregates.Projects
{
    public class Project : AggregateRoot
    {

        public static FluentResults.Result<Project> Create(string title)
        {
            var result = new FluentResults.Result<Project>();

            var titleResult = Title.Create(value: title);

            result.WithErrors(errors: titleResult.Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var resultValue = new Project(titleResult.Value);

            result.WithValue(value: resultValue);

            return result;
        }

        private Project() : base()
        {
            _tasks =
                new System.Collections.Generic.List<Task>();

            _personnelProjects =
              new System.Collections.Generic.List<PersonnelProject>();
        }

        private Project(Title title) : this()
        {
            Title = title;
        }

        public Title Title { get; private set; }

        // **********
        private readonly System.Collections.Generic.List<Task> _tasks;

        public virtual System.Collections.Generic.IReadOnlyList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }
        // **********


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

        public FluentResults.Result Update(string title)
        {
            var result = Create(title);

            result.WithErrors(errors: result.Errors);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Title = result.Value.Title;

            return result.ToResult();

        }

        #region Add Task
        public FluentResults.Result
            <Task>
            AddTask(string title)
        {
            var result = new FluentResults.Result<Task>();

            var taskResult = Task.Create(project: this, title: title);

            if (taskResult.IsFailed)
            {
                result.WithErrors(errors: taskResult.Errors);
            }

            if (result.IsFailed)
            {
                return result;
            }

            // **************************************************
            var hasAny =
                _tasks
                .Where(current => current.Title.Value.ToLower()
                    == taskResult.Value.Title.Value.ToLower())
                .Any();

            if (hasAny)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.Task);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _tasks.Add(item: taskResult.Value);

            result.WithValue(value: taskResult.Value);

            return result;
        }
        #endregion
    }
}
