using FluentValidation;

namespace ViewModels.Users
{
	public class UserManageStatisticReportInputParamsViewModelValidator : AbstractValidator<UserManageStatisticReportInputParamsViewModel>
	{
		public UserManageStatisticReportInputParamsViewModelValidator()
		{
			RuleFor(v => v.UserId)
				.NotEmpty()
				.WithName(Resources.DataDictionary.User)
				.WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
				;

			//RuleFor(v => v.FromDate)
			//.NotEmpty()
			//.WithName(Resources.DataDictionary.StartDate)
			//		.WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

			//RuleFor(v => v.ToDate)
			//.NotEmpty()
			//.WithName(Resources.DataDictionary.EndDate)
			//		.WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.ToDate)
                .GreaterThanOrEqualTo(q => q.FromDate.Value)
                .WithMessage(string.Format(Resources.Messages.Validations.GreaterThanOrEqualTo_TwoFields, Resources.DataDictionary.ToDate, Resources.DataDictionary.FromDate))
                .When(q => q.FromDate.HasValue && q.ToDate.HasValue)
                ;
        }
	}
}
