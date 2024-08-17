namespace Domain.Rich.Aggregates.Projects.ValueObjects
{
    public class Title : SharedKernel.Title
    {
        public static FluentResults.Result<Title> Create(string value)
        {
            var result = new FluentResults.Result<Title>();

            var titleResult = Create(value: value, caption: Resources.DataDictionary.ProjectTitle);

            result.WithErrors(errors: titleResult.Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
               new Title(value: titleResult.Value.Value);

            result.WithValue(value: returnValue);

            return result;

        }

        private Title() : base()
        {
        }

        private Title(string value) : base(value: value)
        {
        }

    }
}
