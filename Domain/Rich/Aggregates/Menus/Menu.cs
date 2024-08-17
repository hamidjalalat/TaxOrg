using System;
using System.Linq;
using Domain.Rich.SeedWork;
using Domain.Rich.SharedKernel;

namespace Domain.Rich.Aggregates.Menus
{
    public class Menu : AggregateRoot
    {

        public static FluentResults.Result<Menu> Create(string title)
        {
            var result = new FluentResults.Result<Menu>();

            var titleResult = Title.Create(value: title);

            result.WithErrors(errors: titleResult.Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var resultValue = new Menu(titleResult.Value);

            result.WithValue(value: resultValue);

            return result;
        }
        public Menu() : base()
        {
        }

        private Menu(Title title) : this()
        {
            Title = title;
        }

        public Title Title { get; private set; }

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

    }
}
