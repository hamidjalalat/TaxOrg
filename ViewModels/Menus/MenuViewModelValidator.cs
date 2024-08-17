﻿using FluentValidation;
using FluentValidation.Results;

namespace ViewModels.Menus
{
    public class MenuViewModelValidator : AbstractValidator<MenuViewModel>
    {
        public MenuViewModelValidator()
        {
            RuleFor(v => v.Sort)
                .GreaterThan(0).WithMessage(string.Format(Resources.Messages.Validations.GreaterThan_FieldValue, ConstClass.PropertyName, 0))
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName(Resources.DataDictionary.Sort);

            RuleFor(v => v.PageTitle)
                .MaximumLength(255).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 255))
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName(Resources.DataDictionary.PageTitle);

            RuleFor(v => v.PageDescription)
                .MaximumLength(512).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 512))
                .WithName(Resources.DataDictionary.PageDescription);

            //RuleFor(v => v.Url)
            //    .MaximumLength(255).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 255))
            //    .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            //    .WithName(Resources.DataDictionary.Url);

            //RuleFor(v => v.PageIcon)
            //    .MaximumLength(255).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 255))
            //    .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            //    .WithName(Resources.DataDictionary.PageIcon);

            //RuleFor(v => v.MenuIcon)
            //   .MaximumLength(255).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 255))
            //   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            //   .WithName(Resources.DataDictionary.MenuIcon);
        }
    }
}
