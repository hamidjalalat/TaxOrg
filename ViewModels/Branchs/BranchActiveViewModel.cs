using Domain.Anemic.Entities;
using NazmMapping.Mappings;


namespace ViewModels.Branchs
{
    public class BranchActiveViewModel : IMapFrom<Branch>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
