using Domain.Anemic.Entities;
using NazmMapping.Mappings;


namespace ViewModels.RegulationGroups
{
    public class RegulationGroupActiveViewModel : IMapFrom<RegulationGroup>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
