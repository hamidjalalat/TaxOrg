using Domain.Anemic.Entities;
using NazmMapping.Mappings;


namespace ViewModels.Netsales
{
    public class NetsaleActiveViewModel : IMapFrom<Netsale>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
