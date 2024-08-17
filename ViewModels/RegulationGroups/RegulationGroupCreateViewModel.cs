using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;

namespace ViewModels.RegulationGroups
{
    public class RegulationGroupCreateViewModel :IMapFrom<RegulationGroup>
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
