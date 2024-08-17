using Domain.Rich.Aggregates.PersonnelTaskTimes;
using Domain.Rich.Aggregates.Projects;
using Domain.Rich.Aggregates.Tasks.ValueObjects;
using Domain.Rich.SeedWork;

namespace Domain.Rich.Aggregates.Tasks
{
    public class Task : AggregateRoot
    {

        public static FluentResults.Result<Task> Create(Project project, string title)
        {

            var result = new FluentResults.Result<Task>();

            if (project is null)
            {
                string errorMessage = string.Format(Resources.Messages.Validations.Required, Resources.DataDictionary.Project);

                result.WithError(errorMessage: errorMessage);
            }

            // var projectResult = Projects.Project.Create(project.Title.Value);

            // result.WithErrors(errors: projectResult.Errors);


            var titleResult = Title.Create(value: title);

            result.WithErrors(errors: titleResult.Errors);


            if (result.IsFailed)
            {
                return result;
            }

            var resultValue = new Task(project, titleResult.Value);

            result.WithValue(value: resultValue);

            return result;
        }
        private Task() : base()
        {

            _personnelTaskTimes =
               new System.Collections.Generic.List<PersonnelTaskTime>();

        }

        private Task(Project project, Title title) : this()
        {
            Title = title;
            Project = project;
        }

        public virtual Project Project { get; private set; }

        public Title Title { get; private set; }


        // **********
        private readonly System.Collections.Generic.List<PersonnelTaskTime> _personnelTaskTimes;

        public virtual System.Collections.Generic.IReadOnlyList<PersonnelTaskTime> PersonnelTaskTimes
        {
            get
            {
                return _personnelTaskTimes;
            }
        }
        // **********


        public FluentResults.Result Update(string title)
        {
            var result = Create(project: Project, title: title);

            result.WithErrors(errors: result.Errors);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Title = result.Value.Title;

            return result.ToResult();
        }

    }
}
