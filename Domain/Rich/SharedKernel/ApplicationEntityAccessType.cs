#region Step 1
//namespace Domain.SharedKernel
//{
//	public class ApplicationEntityAccessType : SeedWork.Enumeration
//	{
//		#region Constant(s)
//		public const int MaxLength = 50;
//		#endregion /Constant(s)

//		#region Static Member(s)
//		public static readonly ApplicationEntityAccessType Special = new(value: 0, name: Resources.DataDictionary.Special);
//		public static readonly ApplicationEntityAccessType Registered = new(value: 1, name: Resources.DataDictionary.Registered);
//		public static readonly ApplicationEntityAccessType Public = new(value: 2, name: Resources.DataDictionary.Public);

//		public static FluentResults.Result<ApplicationEntityAccessType> GetByValue(int? value)
//		{
//			var result =
//				new FluentResults.Result<ApplicationEntityAccessType>();

//			// **************************************************
//			if (value is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.Required, Resources.DataDictionary.AccessType);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}
//			// **************************************************

//			// **************************************************
//			var accessType =
//				FromValue<ApplicationEntityAccessType>(value: value.Value);

//			if (accessType is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.AccessType);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}
//			// **************************************************

//			result.WithValue(value: accessType);

//			return result;
//		}
//		#endregion /Static Member(s)

//		private ApplicationEntityAccessType() : base()
//		{
//		}

//		private ApplicationEntityAccessType(int value, string name) : base(value: value, name: name)
//		{
//		}
//	}
//}
#endregion /Step 1


namespace Domain.Rich.SharedKernel
{
    public class ApplicationEntityAccessType : Enumeration
    {
        #region Constant(s)
        public const int MaxLength = 50;
        #endregion /Constant(s)

        #region Static Member(s)
        public static readonly ApplicationEntityAccessType Special = new(value: 0, name: Resources.DataDictionary.Special);
        public static readonly ApplicationEntityAccessType Registered = new(value: 1, name: Resources.DataDictionary.Registered);
        public static readonly ApplicationEntityAccessType Public = new(value: 2, name: Resources.DataDictionary.Public);

        public static FluentResults.Result<ApplicationEntityAccessType> GetByValue(int? value)
        {
            var result =
                new FluentResults.Result<ApplicationEntityAccessType>();

            // **************************************************
            var entityAccessTypeResult =
                GetByValue<ApplicationEntityAccessType>
                (value: value, name: Resources.DataDictionary.AccessType);
            // **************************************************

            // **************************************************
            if (entityAccessTypeResult.IsFailed)
            {
                result.WithErrors(errors: entityAccessTypeResult.Errors);

                return result;
            }
            // **************************************************

            var returnValue =
                new ApplicationEntityAccessType
                (value: entityAccessTypeResult.Value.Value,
                name: entityAccessTypeResult.Value.Name);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private ApplicationEntityAccessType() : base()
        {
        }

        private ApplicationEntityAccessType
            (int value, string name) : base(value: value, name: name)
        {
        }
    }
}
