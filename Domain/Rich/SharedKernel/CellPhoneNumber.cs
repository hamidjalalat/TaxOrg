﻿using Domain.Rich.SeedWork;

namespace Domain.Rich.SharedKernel
{
    public class CellPhoneNumber : ValueObject
    {
        #region Constant(s)
        public const int FixLength = 11;

        public const int VerificationKeyFixLength = 6;

        public const string RegularExpression = @"09\d{9}";
        #endregion /Constant(s)

        #region Static Member(s)
        public static FluentResults.Result<CellPhoneNumber> Create(string value)
        {
            var result =
                new FluentResults.Result<CellPhoneNumber>();

            value =
                Dtat.String.Fix(text: value);

            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, Resources.DataDictionary.CellPhoneNumber);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.FixLengthNumeric,
                    Resources.DataDictionary.CellPhoneNumber, FixLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch
                (input: value, pattern: RegularExpression) == false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.RegularExpression,
                    Resources.DataDictionary.CellPhoneNumber);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            var returnValue =
                new CellPhoneNumber(value: value);

            result.WithValue(value: returnValue);

            return result;
        }

        public string GetVerificationKey()
        {
            string result =
                System.Guid.NewGuid()
                .ToString().Replace("-", string.Empty)
                .Substring(startIndex: 0, length: VerificationKeyFixLength);

            return result;
        }
        #endregion /Static Member(s)

        private CellPhoneNumber() : base()
        {
        }

        private CellPhoneNumber(string value) : this()
        {
            Value = value;
            IsVerified = false;
            VerificationKey = GetVerificationKey();

        }

        private CellPhoneNumber
            (string value, bool isVerified, string verificationKey) : this(value: value)
        {
            IsVerified = isVerified;
            VerificationKey = verificationKey;
        }

        public string Value { get; }

        public bool IsVerified { get; }

        public string VerificationKey { get; }

        protected override
            System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return IsVerified;
            yield return VerificationKey;
        }

        public FluentResults.Result<CellPhoneNumber> Verify()
        {
            var result =
                new FluentResults.Result<CellPhoneNumber>();

            if (IsVerified == true)
            {
                result.WithError
                    (errorMessage: Resources.Messages.Errors.CellPhoneNumberAlreadyVerified);

                return result;
            }

            var newObject =
                new CellPhoneNumber
                (value: Value, isVerified: true, verificationKey: VerificationKey);

            result.WithValue(value: newObject);

            result.WithSuccess
                (successMessage: Resources.Messages.Successes.CellPhoneNumberVerified);

            return result;
        }

        public FluentResults.Result<CellPhoneNumber> VerifyByKey(string verificationKey)
        {
            var result =
                new FluentResults.Result<CellPhoneNumber>();

            if (string.Compare(VerificationKey, verificationKey, ignoreCase: false) != 0)
            {
                result.WithError
                    (errorMessage: Resources.Messages.Errors.InvalidVerificationKey);

                return result;
            }

            result = Verify();

            return result;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
