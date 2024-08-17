using Domain.Rich.Aggregates.Personnels;
using Domain.Rich.SeedWork;

namespace Domain.Rich.Aggregates.PersonnelProjects
{
    public class PersonnelProject : AggregateRoot
    {

        public static FluentResults.Result<PersonnelProject> Create(
                                                     Personnel personnel,
                                                     Projects.Project project
            )
        {

            var result =
            new FluentResults.Result<PersonnelProject>();

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
            if (project is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.Project);

                result.WithError(errorMessage: errorMessage);
            }
            // **************************************************


            // **************************************************
            if (result.IsFailed)
            {
                return result;
            }
            // **************************************************

            var returnValue =
                new PersonnelProject
                (
                    personnel: personnel,
                    project: project
                );

            result.WithValue(value: returnValue);

            return result;
        }

        private PersonnelProject() : base()
        {

        }

        private PersonnelProject(
            Personnel personnel,
            Projects.Project project

            ) : this()
        {
            Personnel = personnel;
            Project = project;
        }

        public Personnel Personnel { get; private set; }

        public Projects.Project Project { get; private set; }


    }
}
