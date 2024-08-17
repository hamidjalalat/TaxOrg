using Domain.Anemic.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;

namespace ViewModels.Histories
{
    public class HistoryChangedViewModel : IMapFrom<EntityAutoHistory>
	{
		public EntityState Kind { get; set; }
		public HistoryActionTypeEnum? HistoryActionType { get; set; }
		public bool IsDeleted { get; set; }
		public string Changed { get; set; }
		public object? before { get; set; }
        public object? after { get; set; }
        public object ResultType { get; set; }
	}
}
