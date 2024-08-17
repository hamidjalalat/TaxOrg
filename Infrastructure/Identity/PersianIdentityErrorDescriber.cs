using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class PersianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
            => new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(Resources.Messages.Validations.Duplicate, Resources.DataDictionary.EmailAddress, email),
            };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(Resources.Messages.Validations.Duplicate, Resources.DataDictionary.UserName, userName),
            };

        public override IdentityError InvalidEmail(string email)
            => new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = string.Format($"{Resources.DataDictionary.EmailAddress} {Resources.Messages.Validations.InvalidProperty}", email),
            };

        public override IdentityError DuplicateRoleName(string role)
            => new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = string.Format(Resources.Messages.Validations.DuplicateRoleName, role),
            };

        public override IdentityError InvalidRoleName(string role)
            => new IdentityError()
            {
                Code = nameof(InvalidRoleName),
                Description = string.Format($"{Resources.DataDictionary.Role} {Resources.Messages.Validations.InvalidProperty}", role),
            };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = $"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordRequiresDigit}",
            };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = $"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordRequiresLower}",
            };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = $"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordRequiresUpper}",
            };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordRequiresNonAlphanumeric}",
            };

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format($"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordRequiresUniqueChars}", uniqueChars),
            };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format($"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordTooShort}", length),
            };

        public override IdentityError InvalidUserName(string userName)
            => new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = string.Format($"{Resources.DataDictionary.UserName} {Resources.Messages.Validations.InvalidProperty}", userName),
            };

        public override IdentityError UserNotInRole(string role)
            => new IdentityError()
            {
                Code = nameof(UserNotInRole),
                Description = string.Format(Resources.Messages.Validations.UserNotInRole, role),
            };

        public override IdentityError UserAlreadyInRole(string role)
            => new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(Resources.Messages.Validations.UserAlreadyInRole, role),
            };

        public override IdentityError DefaultError()
            => new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = Resources.Messages.Validations.DefaultError,
            };

        public override IdentityError ConcurrencyFailure()
            => new IdentityError()
            {
                Code = nameof(ConcurrencyFailure),
                Description = Resources.Messages.Validations.ConcurrencyFailure,
            };

        public override IdentityError InvalidToken()
            => new IdentityError()
            {
                Code = nameof(InvalidToken),
                Description = string.Format(Resources.Messages.Validations.InvalidProperty, Resources.DataDictionary.Token),
            };

        public override IdentityError RecoveryCodeRedemptionFailed()
            => new IdentityError()
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = string.Format(Resources.Messages.Validations.InvalidProperty, Resources.DataDictionary.RecoveryCode),
            };

        public override IdentityError UserLockoutNotEnabled()
            => new IdentityError()
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = string.Format(Resources.Messages.Validations.UserLockoutNotEnabled),
            };

        public override IdentityError UserAlreadyHasPassword()
            => new IdentityError()
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = string.Format(Resources.Messages.Validations.UserAlreadyHasPassword, Resources.DataDictionary.Password),
            };

        public override IdentityError PasswordMismatch()
            => new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = string.Format($"{Resources.DataDictionary.Password} {Resources.Messages.Validations.PasswordMismatch}"),
            };

        public override IdentityError LoginAlreadyAssociated()
            => new IdentityError()
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = string.Format(Resources.Messages.Validations.LoginAlreadyAssociated),
            };
    }
}
