using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.RegulationGroups
{
    public class RegulationGroupUpdateViewModel:IMapFrom<RegulationGroup>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
