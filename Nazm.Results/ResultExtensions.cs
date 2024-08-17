
namespace Nazm.Results
{
    public static class ResultExtensions
	{
		static ResultExtensions()
		{
		}

		public static Result ConvertToDtatResult(this FluentResults.Result result)
		{
			Result dtatResult = new()
			{
				IsFailed = result.IsFailed,
				IsSuccess = result.IsSuccess,
			};

			if (result.Errors != null && result.Errors.Count > 0)
			{
				foreach (var item in result.Errors)
				{
					dtatResult.AddErrorMessage(message: item.Message);
				}
			}

			if (result.Successes != null && result.Successes.Count > 0)
			{
				foreach (var item in result.Successes)
				{
					dtatResult.AddSuccessMessage(message: item.Message);
				}
			}

			return dtatResult;
		}

		public static Result<T> ConvertToDtatResult<T>(this FluentResults.Result<T> result)
		{
			Result<T> dtatResult = new()
			{
				IsFailed = result.IsFailed,
				IsSuccess = result.IsSuccess,
			};

			if (result.IsFailed == false)
			{
				dtatResult.Value = result.Value;
			}

			if (result.Errors != null && result.Errors.Count > 0)
			{
				foreach (var item in result.Errors)
				{
					dtatResult.AddErrorMessage(message: item.Message);
				}
			}

			if (result.Successes != null && result.Successes.Count > 0)
			{
				foreach (var item in result.Successes)
				{
					dtatResult.AddSuccessMessage(message: item.Message);
				}
			}

			return dtatResult;
		}
	}
}
