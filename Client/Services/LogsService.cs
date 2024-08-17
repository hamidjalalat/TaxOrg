using Nazm.Results;

namespace Client.Services
{
	public class LogsService : object
	{
		public LogsService() : base()
		{
			Logs =
				new System.Collections.Generic.List<ViewModels.Logs.Log>();
		}

		protected System.Collections.Generic.IList<ViewModels.Logs.Log> Logs { get; }

		public void AddLog(System.Type type, string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			// **************************************************
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();
			// **************************************************
			
			message =
				//$"{ type.Namespace } -> { type.Name } -> { methodBase.Name }: { message.Fix() }";
				$"{ type.Namespace } -> { type.Name } -> { methodBase.Name }: { Nazm.String.Fix(message) }";

			var log =
				new ViewModels.Logs.Log(message: message);

			//Logs.Add(log);
			Logs.Insert(index: 0, item: log);
		}

		public void AddLog(System.Type type, ViewModels.Logs.Log log)
		{
			if (log == null)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(log.Message))
			{
				return;
			}

			// **************************************************
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();
			// **************************************************

			log.Message =
				//$"{ type.Namespace } -> { type.Name } -> { methodBase.Name }: { log.Message.Fix() }";
				$"{ type.Namespace } -> { type.Name } -> { methodBase.Name }: {Nazm.String.Fix(log.Message) }";

			Logs.Insert(index: 0, item: log);
		}

		public System.Collections.Generic.IList<ViewModels.Logs.Log> GetLogs()
		{
			return Logs;
		}

		public void ClearLogs()
		{
			Logs.Clear();
		}
	}
}
