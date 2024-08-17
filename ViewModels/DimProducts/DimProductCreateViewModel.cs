using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;

namespace ViewModels.DimProducts
{
    public class DimProductCreateViewModel :IMapFrom<DimProduct>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
