using Domain.Anemic.Common;
using EntityFrameworkCore.AutoHistory.Attributes;

namespace Domain.Anemic.Entities
{
	[ExcludeFromHistory]
	public class ActionMethod : Entity
	{
		public int ActionMethodId { get; set; }
		public int ControllerId { get; set; }
		public Controller? Controller { get; set; }
		public string TitleFa { get; set; }
		public string TitleEn { get; set; }
		public bool IsShow { get; set; }
	}
}
