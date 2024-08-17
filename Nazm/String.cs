
namespace Nazm
{
	public static class String
	{
		static String()
		{
		}

		public static string Fix(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}

			text =
				text.Trim();

			while (text.Contains("  "))
			{
				text =
					text.Replace("  ", " ");
			}

			return text;
		}

		// نوشتن تابع ذیل مطلقا توصیه نمی‌شود String دقت کنید که به دلیل احتمال نال بودن متن
		//public static string Fix(this string text)
		//{
		//	if (text is null)
		//	{
		//		return null;
		//	}

		//	text =
		//		text.Trim();

		//	if (text == string.Empty)
		//	{
		//		return null;
		//	}

		//	while (text.Contains("  "))
		//	{
		//		text =
		//			text.Replace("  ", " ");
		//	}

		//	return text;
		//}
	}
}
