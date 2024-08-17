//namespace Domain.SharedKernel
//{
//	public class Activation : SeedWork.ValueObject
//	{
//		#region Static Member(s)
//		public static FluentResults.Result<Activation> Create(bool? value)
//		{
//			var result =
//				new FluentResults.Result<Activation>();

//			// **************************************************
//			if (value is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.Required, Resources.DataDictionary.IsActive);

//				result.WithError(errorMessage: errorMessage);
//			}
//			// **************************************************

//			// **************************************************
//			if (result.IsFailed)
//			{
//				return result;
//			}
//			// **************************************************

//			var returnValue =
//				new Activation(isActive: value.Value);

//			result.WithValue(value: returnValue);

//			return result;
//		}
//		#endregion /Static Member(s)

//		private Activation() : base()
//		{
//		}

//		private Activation(bool isActive) : this()
//		{
//			IsActive = isActive;

//			Timestamp =
//				DateTime.Create(value: SeedWork.Utility.Now).Value;
//		}

//		public bool IsActive { get; }

//		public DateTime Timestamp { get; }

//		protected override
//			System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
//		{
//			yield return IsActive;
//			yield return Timestamp;
//		}

//		internal Activation Activate()
//		{
//			var result =
//				Create(value: true);

//			return result.Value;
//		}

//		internal Activation Deactivate()
//		{
//			var result =
//				Create(value: false);

//			return result.Value;
//		}

//		public override string ToString()
//		{
//			string result =
//				$"{ IsActive } - {Timestamp:MM/dd/yyyy HH:mm:ss}";

//			return result;
//		}
//	}
//}
