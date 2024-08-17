#region Step 1
//namespace Domain.SharedKernel
//{
//	public class ApplicationEntityType : SeedWork.Enumeration
//	{
//		#region Constant(s)
//		public const int MaxLength = 50;
//		#endregion /Constant(s)

//		#region Static Member(s)
//		public static readonly ApplicationEntityType Action = new(value: 0, name: Resources.DataDictionary.Action);
//		public static readonly ApplicationEntityType Claim = new(value: 1, name: Resources.DataDictionary.Claim);

//		public static FluentResults.Result<ApplicationEntityType> GetByValue(int? value)
//		{
//			var result =
//				new FluentResults.Result<ApplicationEntityType>();

//			// **************************************************
//			if (value is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.Required, Resources.DataDictionary.EntityType);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}
//			// **************************************************

//			// **************************************************
//			var entityType =
//				FromValue<ApplicationEntityType>(value: value.Value);

//			if (entityType is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.EntityType);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}
//			// **************************************************

//			result.WithValue(value: entityType);

//			return result;
//		}
//		#endregion /Static Member(s)

//		private ApplicationEntityType() : base()
//		{
//		}

//		private ApplicationEntityType(int value, string name) : base(value: value, name: name)
//		{
//		}
//	}
//}
#endregion Step 1


namespace Domain.Rich.SharedKernel
{
    public class ApplicationEntityType : Enumeration
    {
        #region Constant(s)
        public const int MaxLength = 50;
        #endregion /Constant(s)

        #region Static Member(s)
        public static readonly ApplicationEntityType Action = new(value: 0, name: Resources.DataDictionary.Action);
        public static readonly ApplicationEntityType Claim = new(value: 1, name: Resources.DataDictionary.Claim);

        public static FluentResults.Result<ApplicationEntityType> GetByValue(int? value)
        {
            var result =
                new FluentResults.Result<ApplicationEntityType>();

            // **************************************************
            var entityTypeResult =
                GetByValue<ApplicationEntityType>
                (value: value, name: Resources.DataDictionary.EntityType);
            // **************************************************

            // **************************************************
            if (entityTypeResult.IsFailed)
            {
                result.WithErrors(errors: entityTypeResult.Errors);

                return result;
            }
            // **************************************************

            var returnValue =
                new ApplicationEntityType
                (value: entityTypeResult.Value.Value,
                name: entityTypeResult.Value.Name);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private ApplicationEntityType() : base()
        {
        }

        private ApplicationEntityType(int value, string name) : base(value: value, name: name)
        {
        }
    }
}
