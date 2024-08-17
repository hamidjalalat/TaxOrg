using Domain.Anemic.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Anemic.Entities
{
    public class RegulationGroup : Entity
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
